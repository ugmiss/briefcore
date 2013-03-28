// Upload to codeproject.com
// By Ibraheem AlKilanny
// d3_ib@hotmail.com - http://sites.google.com/site/ibraheemalkilany/
// all rights reserved 2011

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Path_Finding
{
    /// <summary>
    /// Creates and traverse a graph using several algorithms
    /// </summary>
    internal class Graph : IDisposable
    {
        /// <summary>
        /// Initializes new instance of Graph
        /// </summary>
        /// <param name="maxWidth">The total nodes per width</param>
        /// <param name="maxHeight">The total nodes per height</param>
        /// <param name="initialSize">The initial graph size</param>
        public Graph(int maxWidth, int maxHeight, Size initialSize)
        {
            this.verteces = new Vertex[maxHeight, maxWidth];
            this.Resize(initialSize);
        }

        /// <summary>
        /// Used to slow path finding process
        /// </summary>
        public int Sleep;

        /// <summary>
        /// Gets the last found path's length
        /// </summary>
        public int PathLength
        {
            get;
            private set;
        }

        /// <summary>
        /// Represents algorithms used by Graph class
        /// </summary>
        public enum Algorithms
        {
            /// <summary>
            /// Find path with Dijkstra's algorithm
            /// </summary>
            Dijkstra,
            /// <summary>
            /// Find path with A*-Manhattan algorithm
            /// </summary>
            AStarManhattan,
            /// <summary>
            /// Find path with bi-directional version of Dijkstra's algorithm
            /// </summary>
            BiDirectionalDijkstra,
            /// <summary>
            /// Find path with bi-directional version of A*-Manhattan algorithm
            /// </summary>
            BiDirectionalAStarManhattan
        }

        /// <summary>
        /// Gets the graph width and height
        /// </summary>
        public Size Size
        {
            //get { return new Size(this.verteces.GetLength(1), this.verteces.GetLength(0)); }
            get;
            private set;
        }

        /// <summary>
        /// A two-dimensional array for storing Vertices of the graph
        /// </summary>
        private readonly Vertex[,] verteces;

        /// <summary>
        /// Raises when the process of finding path has been completed
        /// </summary>
        public event EventHandler PathFound;

        /// <summary>
        /// Gets or sets the position of start point for the path finding
        /// </summary>
        public Point Start;

        /// <summary>
        /// Gets or sets the position of end point for the path finding
        /// </summary>
        public Point End;

        /// <summary>
        /// Used to draw the found path
        /// </summary>
        private Pen penPath = new Pen(Brushes.White, 5);

        /// <summary>
        /// Used to write numbers (costs) while the path finding
        /// </summary>
        private Font font;

        /// <summary>
        /// Gets or sets a set of found path vertices from start point to end point
        /// </summary>
        private List<Vertex> foundPath = new List<Vertex>();

        /// <summary>
        /// Measures the distance between two neighbour vertices horizontally or vertically
        /// </summary>
        private const int regularDistance = 10;

        /// <summary>
        /// Measures the distance between two neighbour vertices diagonally
        /// </summary>
        private const int diagonalDistance = 14;

        /// <summary>
        /// Resizes the graph to a size deosn't exceed the maximum size
        /// </summary>
        /// <param name="size">The new size</param>
        public void Resize(Size size)
        {
            if (size.Width > this.verteces.GetLength(1)
                || size.Height > this.verteces.GetLength(0))
                throw new ArgumentOutOfRangeException();

            int x, y = 0;
            for (int i = 0; i < size.Height; i++)
            {
                x = 0;
                // we must make new objects, since the x and y have been changed
                for (int j = 0; j < size.Width; j++)
                {
                    if (j == size.Width / 2 && i > 0 && i < size.Height - 1)
                        this.verteces[i, j] = new Vertex(new Point(x, y), new Point(j, i), false);
                    else
                        this.verteces[i, j] = new Vertex(new Point(x, y), new Point(j, i), true);
                    x += Vertex.Size;
                }
                y += Vertex.Size;
            }

            this.Size = size;
            this.Start = new Point(0, size.Height / 2);
            this.End = new Point(size.Width - 1, size.Height / 2);

            this.font = new Font("Arial", Vertex.Size / 5);
        }

        /// <summary>
        /// Used to slow operations
        /// </summary>
        /// <param name="times">Number of times to slow</param>
        private void sleep(int times)
        {
            Thread.SpinWait(times * 100000);
        }

        /// <summary>
        /// Draws an arrow from the parent node to the child
        /// </summary>
        /// <param name="g">A graphics object to draw onto</param>
        /// <param name="parent">The position on the graphics surface for the parent node</param>
        /// <param name="child">The position on the graphics surface for the child node</param>
        /// <param name="ellipSize">Size of the ellipse drawn on the child node</param>
        private void drawParent(Graphics g, Point parent, Point child, int ellipSize)
        {
            g.DrawLine(Pens.Tan, parent, child);
            g.DrawEllipse(Pens.Tan, child.X, child.Y, ellipSize, ellipSize);
        }

        /// <summary>
        /// Draws the graph on a specific Graphics with a specific algorithm
        /// </summary>
        /// <param name="g">A surface to draw onto</param>
        /// <param name="algorithm">Indicates the used algorithm for path finding</param>
        public void Draw(Graphics g, Algorithms algorithm)
        {
            g.Clear(Color.Black);
            int half = Vertex.Size / 2;

            // loop on every cell in the 2d array
            for (int i = 0; i < this.Size.Height; i++)
            {
                for (int j = 0; j < this.Size.Width; j++)
                {
                    // Walls
                    if (!this.verteces[i, j].Walkable)
                    {
                        g.FillRectangle(Brushes.DarkBlue, new Rectangle(this.verteces[i, j].Location, new Size(Vertex.Size, Vertex.Size)));
                        continue;
                    }

                    #region Node state
                    if (this.verteces[i, j].Status1 != Vertex.Statuses.Unvisited 
                        || this.verteces[i, j].Status2 != Vertex.Statuses.Unvisited)
                    {
                        if (this.verteces[i, j].Status1 == Vertex.Statuses.ClosedList
                            || this.verteces[i, j].Status2 == Vertex.Statuses.ClosedList)
                        {
                            g.DrawRectangle(Pens.Silver, new Rectangle(this.verteces[i, j].Location, new Size(Vertex.Size, Vertex.Size)));
                        }
                        else if (this.verteces[i, j].Status1 == Vertex.Statuses.OpenList
                            || this.verteces[i, j].Status2 == Vertex.Statuses.OpenList)
                        {
                            g.FillRectangle(Brushes.Gray, new Rectangle(this.verteces[i, j].Location, new Size(Vertex.Size, Vertex.Size)));
                            g.DrawRectangle(Pens.CadetBlue, new Rectangle(this.verteces[i, j].Location, new Size(Vertex.Size, Vertex.Size)));
                        }

                        #region Cost drawing
                        // Draw the costs of G, F and H if A* is used.
                        if (this.verteces[i, j].StartToEnd)
                            g.DrawString(this.verteces[i, j].G1.ToString(), this.font, Brushes.White,
                                new PointF(this.verteces[i, j].Location.X + half / 8, this.verteces[i, j].Location.Y + half * 3 / 2));
                        else
                            g.DrawString(this.verteces[i, j].G2.ToString(), this.font, Brushes.White,
                                new PointF(this.verteces[i, j].Location.X + half / 8, this.verteces[i, j].Location.Y + half * 3 / 2));

                        if (algorithm == Algorithms.AStarManhattan)
                        {
                            g.DrawString(this.verteces[i, j].F1.ToString(), this.font, Brushes.White,
                                new PointF(this.verteces[i, j].Location.X + half / 8, this.verteces[i, j].Location.Y + half / 8));
                            g.DrawString(this.verteces[i, j].H1.ToString(), this.font, Brushes.White,
                                new PointF(this.verteces[i, j].Location.X + half * 2 / 3, this.verteces[i, j].Location.Y + half * 2 / 3));
                        }
                        else if (algorithm == Algorithms.BiDirectionalAStarManhattan)
                        {
                            int f, h;
                            if (this.verteces[i, j].StartToEnd)
                            {
                                h = this.verteces[i, j].H1;
                                f = this.verteces[i, j].F1;
                            }
                            else
                            {
                                h = this.verteces[i, j].H2;
                                f = this.verteces[i, j].F2;
                            }
                            g.DrawString(f.ToString(), this.font, Brushes.White,
                                   new PointF(this.verteces[i, j].Location.X + half / 8, this.verteces[i, j].Location.Y + half / 8));
                            g.DrawString(h.ToString(), this.font, Brushes.White,
                                new PointF(this.verteces[i, j].Location.X + half * 2 / 3, this.verteces[i, j].Location.Y + half * 2 / 3));
                        }
                        #endregion
                    } // end (Status != Vertex.Statuses.Unvisited)
                    else // (Status == Vertex.Statuses.Unvisited)
                    {
                        g.DrawRectangle(Pens.CadetBlue, new Rectangle(this.verteces[i, j].Location, new Size(Vertex.Size, Vertex.Size)));
                    }
                    #endregion

                    #region Parent arrows
                    Vertex parent = this.verteces[i, j].StartToEnd ? this.verteces[i, j].Parent1 : this.verteces[i, j].Parent2;
                    if (parent != null)
                    {
                        // draw the arrow between the middles of the parent and child nodes
                        this.drawParent(g,
                            new Point(parent.Location.X + half, parent.Location.Y + half),
                            new Point(this.verteces[i, j].Location.X + half, this.verteces[i, j].Location.Y + half),
                            half / 4);
                    }
                    #endregion
                }
            }

            #region Start and End
            g.FillRectangle(Brushes.SpringGreen, 
                new Rectangle(this.verteces[this.Start.Y, this.Start.X].Location,
                    new Size(Vertex.Size, Vertex.Size)));
            g.FillRectangle(Brushes.Red,
                new Rectangle(this.verteces[this.End.Y, this.End.X].Location,
                    new Size(Vertex.Size, Vertex.Size)));
            #endregion
        }

        /// <summary>
        /// Generate random walls on the graph
        /// </summary>
        public void MakeRandomWalls()
        {
            Random random = new Random();
            for (int i = 0; i < this.Size.Height; i++)
            {
                for (int j = 0; j < this.Size.Width; j++)
                {
                    if (random.Next(4) == 0)
                        this.verteces[i, j] = new Vertex(this.verteces[i, j].Location, this.verteces[i, j].Position, false);
                }
            }

            if (!this.verteces[this.Start.Y, this.Start.X].Walkable)
                this.verteces[this.Start.Y, this.Start.X] =
                    new Vertex(this.verteces[this.Start.Y, this.Start.X].Location,
                    this.Start,
                    true);
            if (!this.verteces[this.End.Y, this.End.X].Walkable)
                this.verteces[this.End.Y, this.End.X] = 
                    new Vertex(this.verteces[this.End.Y, this.End.X].Location,
                    this.End,
                    true);
        }

        /// <summary>
        /// Reset the graph to its initial state
        /// </summary>
        /// <param name="walls">True to remove all walls</param>
        public void Reset(bool walls)
        {
            for (int i = 0; i < this.Size.Height; i++)
            {
                for (int j = 0; j < this.Size.Width; j++)
                {
                    if (!this.verteces[i, j].Walkable && walls)
                        this.verteces[i, j] = new Vertex(this.verteces[i, j].Location, this.verteces[i, j].Position, true);
                    else
                    {
                        this.verteces[i, j].Status1 = Vertex.Statuses.Unvisited;
                        this.verteces[i, j].Status2 = Vertex.Statuses.Unvisited;
                        this.verteces[i, j].Parent1 = null;
                        this.verteces[i, j].Parent2 = null;
                        this.verteces[i, j].G1 = this.verteces[i, j].H1 = 0;
                        this.verteces[i, j].G2 = this.verteces[i, j].H2 = 0;
                    }
                }
            }
            this.PathLength = 0;
        }

        /// <summary>
        /// Inserts a vertex into a sorted list using a modified binary search
        /// </summary>
        /// <param name="list">A list to insert in</param>
        /// <param name="ver">A vertex to insert</param>
        /// <param name="startToEnd">True if the path finding is from start to end</param>
        private void insert(List<Vertex> list, Vertex ver, bool startToEnd)
        {
            // although this algorithm is slower than linear search in the worst case O(n + log n)
            // it is much faster in the average case (log n)
            if (list.Count == 0)
            {
                list.Add(ver);
                return;
            }

            // the startToEnd and endToStart cases are the same expect using 'costs-2' instead of 'costs-1'
            
            int start = 0,
                end = list.Count - 1,
                middle = (end + start + 1) / 2;
            bool found = false;
            if (startToEnd)
            {
                #region Start to end
                // binary search
                while (start <= end && !found)
                {
                    if (list[middle].F1 == ver.F1)
                        found = true;
                    else if (list[middle].F1 < ver.F1)
                        start = middle + 1;
                    else
                        end = middle - 1;
                    middle = (end + start + 1) / 2;
                }

                if (found)
                {
                    // back to the first duplicate not lower in costs than me
                    middle--;
                    while (middle >= 0 && list[middle].F1 == ver.F1 && list[middle].H1 > ver.H1 && list[middle].G1 < ver.G1)
                        middle--;
                    middle++;

                    // check H costs
                    while (middle < list.Count && list[middle].F1 == ver.F1 && list[middle].H1 < ver.H1)
                        middle++;

                    // check G costs
                    while (middle < list.Count && list[middle].F1 == ver.F1 && list[middle].H1 == ver.H1 && list[middle].G1 > ver.G1)
                        middle++;

                    list.Insert(middle, ver);
                }
                else if (start < list.Count && list[start].F1 > ver.F1)
                    list.Insert(start, ver);
                else
                    list.Insert(end + 1, ver);
                #endregion
            }
            else
            {
                #region end to start
                while (start <= end && !found)
                {
                    if (list[middle].F2 == ver.F2)
                        found = true;
                    else if (list[middle].F2 < ver.F2)
                        start = middle + 1;
                    else
                        end = middle - 1;
                    middle = (end + start + 1) / 2;
                }

                if (found)
                {
                    middle--;
                    while (middle >= 0 && list[middle].F2 == ver.F2 && list[middle].H2 > ver.H2 && list[middle].G2 < ver.G2)
                        middle--;
                    middle++;

                    while (middle < list.Count && list[middle].F2 == ver.F2 && list[middle].H2 < ver.H2)
                        middle++;

                    while (middle < list.Count && list[middle].F2 == ver.F2 && list[middle].H2 == ver.H2 && list[middle].G2 > ver.G2)
                        middle++;

                    list.Insert(middle, ver);
                }
                else if (start < list.Count && list[start].F2 > ver.F2)
                    list.Insert(start, ver);
                else
                    list.Insert(end + 1, ver);
                #endregion
            }
        }
        
        /// <summary>
        /// Calculates the exact distance between two vertices
        /// </summary>
        /// <param name="x">The first vertex</param>
        /// <param name="y">The second vertex</param>
        /// <returns></returns>
        private int calculateDistance(Vertex x, Vertex y)
        {
            if (x.Position.X == y.Position.X)
                return regularDistance * Math.Abs(x.Position.Y - y.Position.Y);
            else if (x.Position.Y == y.Position.Y)
                return regularDistance * Math.Abs(x.Position.X - y.Position.X);
            else
                return diagonalDistance * Math.Abs(x.Position.X - y.Position.X);
        }

        /// <summary>
        /// Draws the found path
        /// </summary>
        /// <param name="g">The surface to draw onto</param>
        public void DrawPath(Graphics g)
        {
            this.PathLength = 0;
            int h = Vertex.Size / 2;
            for (int i = 1; i < this.foundPath.Count; i++)
            {
                g.DrawLine(this.penPath, 
                    new Point(this.foundPath[i].Location.X + h, this.foundPath[i].Location.Y + h),
                    new Point(this.foundPath[i - 1].Location.X + h, this.foundPath[i - 1].Location.Y + h));
                this.PathLength += this.calculateDistance(this.foundPath[i], this.foundPath[i - 1]);
            }
        }

        /// <summary>
        /// Returns a vertex in the 2d array according to its position in it
        /// </summary>
        /// <param name="position">The position of the vertex in the 2d array</param>
        /// <returns></returns>
        private Vertex getVertex(ref Point position)
        {
            return this.verteces[position.Y, position.X];
        }

        /// <summary>
        /// Find path with bi-directional algorithms
        /// </summary>
        /// <param name="alg">The used algorithm</param>
        private void biDirectionalPathFinding(Algorithms alg)
        {
            // using two lists instead of one
            List<Vertex> openListStartEnd = new List<Vertex>();
            List<Vertex> openListEndStart = new List<Vertex>();

            Vertex end = this.getVertex(ref this.End);
            Vertex first = this.getVertex(ref this.Start);

            Vertex currentStartEnd = first;
            Vertex currentEndStart = end;

            currentStartEnd.Status1 = Vertex.Statuses.OpenList;
            currentEndStart.Status2 = Vertex.Statuses.OpenList;

            this.insert(openListStartEnd, currentStartEnd, true);
            this.insert(openListEndStart, currentEndStart, false);

            bool pathFoundEndToStart = false;
            bool pathFoundStartToEnd = false;

            while (openListStartEnd.Count > 0 && openListEndStart.Count > 0)
            {
                currentStartEnd = openListStartEnd[0];
                currentEndStart = openListEndStart[0];

                openListStartEnd.RemoveAt(0);
                openListEndStart.RemoveAt(0);

                if (currentEndStart.Status1 == Vertex.Statuses.ClosedList)
                {
                    pathFoundEndToStart = true;
                    break;
                }
                if (currentStartEnd.Status2 == Vertex.Statuses.ClosedList)
                {
                    pathFoundStartToEnd = true;
                    break;
                }

                currentStartEnd.Status1 = Vertex.Statuses.ClosedList;
                currentEndStart.Status2 = Vertex.Statuses.ClosedList;

                if (currentStartEnd.Walkable)
                {
                    currentStartEnd.StartToEnd = true;
                    this.checkAdjacents(openListStartEnd, currentStartEnd, end, alg);
                }
                if (currentEndStart.Walkable)
                {
                    currentEndStart.StartToEnd = false;
                    this.checkAdjacents(openListEndStart, currentEndStart, first, alg);
                }

                this.sleep(this.Sleep);
            }

            // Have we found a path?
            if (pathFoundEndToStart || pathFoundStartToEnd)
            {
                this.foundPath.Clear();
                // Collect the found path from the parents
                if (pathFoundStartToEnd)
                {
                    Vertex hold = currentStartEnd;

                    while (currentStartEnd != end)
                    {
                        this.foundPath.Add(currentStartEnd);
                        currentStartEnd = currentStartEnd.Parent2;
                    }
                    this.foundPath.Add(currentStartEnd);

                    currentStartEnd = hold.Parent1;
                    while (currentStartEnd != first)
                    {
                        this.foundPath.Insert(0, currentStartEnd);
                        currentStartEnd = currentStartEnd.Parent1;
                    }
                    this.foundPath.Insert(0, currentStartEnd);
                }
                else
                {
                    Vertex hold = currentEndStart;

                    while (currentEndStart != first)
                    {
                        this.foundPath.Add(currentEndStart);
                        currentEndStart = currentEndStart.Parent1;
                    }
                    this.foundPath.Add(currentEndStart);

                    currentEndStart = hold.Parent2;
                    while (currentEndStart != end)
                    {
                        this.foundPath.Insert(0, currentEndStart);
                        currentEndStart = currentEndStart.Parent2;
                    }
                    this.foundPath.Insert(0, currentEndStart);
                }
                
                EventHandler temp = null;
                while (temp == null)
                {
                    temp = this.PathFound;
                }
                temp(this, null);
            }
            else // We have failed to find path
            {
                currentStartEnd.Status1 = Vertex.Statuses.ClosedList;
                currentEndStart.Status2 = Vertex.Statuses.ClosedList;
            }
        }

        /// <summary>
        /// Finds path using Dijkstra's and A* algorithms
        /// </summary>
        /// <param name="algorithm">The used algorithm</param>
        public void PathFinding(Algorithms algorithm)
        {
            if (algorithm == Algorithms.BiDirectionalDijkstra 
                || algorithm == Algorithms.BiDirectionalAStarManhattan)
            {
                this.biDirectionalPathFinding(algorithm);
                return;
            }
            List<Vertex> openList = new List<Vertex>();
            Vertex goal = this.getVertex(ref this.End);
            Vertex first = this.getVertex(ref this.Start);

            Vertex current = first;
            bool pathFound = false;
            current.Status1 = Vertex.Statuses.OpenList;
            
            this.insert(openList, current, true);
            
            // Repeat while the openlist is not empty and you haven't reached the end
            while (openList.Count > 0)
            {
                // since the openlist is sorted,
                // we have always the lowest F cost at 0 index
                current = openList[0];
                openList.RemoveAt(0);

                // mark as closed (i.e. we won't back to it again)
                current.Status1 = Vertex.Statuses.ClosedList;
                if (current == goal)
                {
                    // we have found the end
                    pathFound = true;
                    break;
                }

                // don't visit a wall!
                if (current.Walkable)
                {
                    // we have it always true, since we are not using bi-directional
                    current.StartToEnd = true;
                    this.checkAdjacents(openList, current, goal, algorithm);
                }

                this.sleep(this.Sleep);
            }

            if (pathFound)
            {
                // collect path
                this.foundPath.Clear();
                Vertex last = current;
                while (current != first)
                {
                    this.foundPath.Add(current);
                    if (current.Parent1 == last)
                        break;
                    last = current;
                    current = current.Parent1;
                }
                this.foundPath.Add(current);

                // fire the event
                EventHandler temp = null;
                while (temp == null)
                {
                    temp = this.PathFound;
                }
                temp(this, null);
            }
            else
            {
                current.Status1 = Vertex.Statuses.ClosedList;
            }
        }

        /// <summary>
        /// Determines whether a point is a valid walkable position on the Graph
        /// </summary>
        /// <param name="next">The next position to check</param>
        /// <param name="current">The current position</param>
        /// <param name="ver">To return the vertex if the position exists</param>
        /// <returns>True if the position exists</returns>
        private bool validLocation(ref Point next, Point current, out Vertex ver)
        {
            // bounds checking
            if (next.X < this.Size.Width && next.Y < this.Size.Height
                && next.X >= 0 && next.Y >= 0)
            {
                ver = this.getVertex(ref next);
                if (!ver.Walkable)
                    return false;

                #region Walls checking
                // this checking-path is faster (7 checks at most)
                if (next.X == current.X + 1)
                {
                    // next is bottom-right
                    if (next.Y == current.Y + 1)
                    {
                        if (current.X + 1 < this.Size.Width && !this.verteces[current.Y, current.X + 1].Walkable &&
                            current.Y + 1 < this.Size.Height && !this.verteces[current.Y + 1, current.X].Walkable)
                            return false;
                    }
                    // next is top-right
                    else if (next.Y == current.Y - 1)
                    {
                        if (current.X + 1 < this.Size.Width && !this.verteces[current.Y, current.X + 1].Walkable &&
                            current.Y - 1 >= 0 && !this.verteces[current.Y - 1, current.X].Walkable)
                            return false;
                    }
                }
                else if (next.X == current.X - 1)
                {
                    // next is top-left
                    if (next.Y == current.Y - 1)
                    {
                        if (current.X - 1 >= 0 && !this.verteces[current.Y, current.X - 1].Walkable &&
                            current.Y - 1 >= 0 && !this.verteces[current.Y - 1, current.X].Walkable)
                            return false;
                    }
                    // next is bottom-right
                    else if (next.Y == current.Y + 1)
                    {
                        if (current.X - 1 >= 0 && !this.verteces[current.Y, current.X - 1].Walkable &&
                            current.Y + 1 < this.Size.Height && !this.verteces[current.Y + 1, current.X].Walkable)
                            return false;
                    }
                }
                #endregion

                return true;
            }
            ver = null;
            return false;
        }

        /// <summary>
        /// Estimates distance between two nodes on the graph (used by A* to estimate H cost)
        /// </summary>
        /// <param name="postion1"></param>
        /// <param name="position2"></param>
        /// <returns></returns>
        private int estimateDistance(Point postion1, Point position2)
        {
            return (Math.Abs(postion1.X - position2.X) + Math.Abs(postion1.Y - position2.Y)) * regularDistance;
        }

        /// <summary>
        /// Check the adjacent nodes and adds them to the open list
        /// </summary>
        /// <param name="open">The open list</param>
        /// <param name="node">The current node to check adjacents</param>
        /// <param name="target">The end node (used by A* to estimate H cost)</param>
        /// <param name="alg">The used algorithm</param>
        private void checkAdjacents(List<Vertex> open, Vertex node, Vertex target, Algorithms alg)
        {
            int temp;
            for (int i = 0; i < node.Adjacents.Length; i++)
            {
                Vertex next;
                if (this.validLocation(ref node.Adjacents[i], node.Position, out next))
                {
                    if ((node.StartToEnd && next.Status1 == Vertex.Statuses.Unvisited) ||
                        (!node.StartToEnd && next.Status2 == Vertex.Statuses.Unvisited))
                    {
                        #region Not visited
                        if (node.StartToEnd)
                        {
                            next.Status1 = Vertex.Statuses.OpenList;
                            next.Parent1 = node;
                        }
                        else
                        {
                            next.Status2 = Vertex.Statuses.OpenList;
                            next.Parent2 = node;
                        }

                        if (node.StartToEnd)
                        {
                            if (i % 2 != 0) // is it diagonal?
                                next.G1 = node.G1 + diagonalDistance;
                            else
                                next.G1 = node.G1 + regularDistance;
                            if (alg == Algorithms.AStarManhattan || alg == Algorithms.BiDirectionalAStarManhattan)
                                next.H1 = this.estimateDistance(next.Position, target.Position);
                        }
                        else // form end to start
                        {
                            if (i % 2 != 0)
                                next.G2 = node.G2 + diagonalDistance;
                            else
                                next.G2 = node.G2 + regularDistance;
                            if (alg == Algorithms.BiDirectionalAStarManhattan)
                                next.H2 = this.estimateDistance(next.Position, target.Position);
                        }
                        this.insert(open, next, node.StartToEnd);
                        #endregion
                    }
                    else
                    {
                        #region Already visited
                        // is it diagonal
                        if (i % 2 == 0)
                            temp = regularDistance;
                        else
                            temp = diagonalDistance;

                        // is the new distance smaller?
                        if (node.StartToEnd)
                        {
                            if (node.G1 + temp < next.G1)
                            {
                                next.Parent1 = node;
                                next.G1 = node.G1 + temp;
                                // the sort has been changed
                                // this makes more sence because it takes O(n log n) rather than O(2 n)
                                open.Remove(next);
                                this.insert(open, next, node.StartToEnd);
                            }
                        }
                        else if (node.G2 + temp < next.G2)
                        {
                            next.Parent2 = node;
                            next.G2 = node.G2 + temp;
                            open.Remove(next);
                            this.insert(open, next, node.StartToEnd);
                        }
                        #endregion
                    } // end visited
                } // end valid location
            } // end for
        }

        /// <summary>
        /// Returs a vertex in the graph with respect to its position
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Vertex GetVertex(Point p)
        {
            for (int i = 0; i < this.Size.Height; i++)
            {
                for (int j = 0; j < this.Size.Width; j++)
                {
                    if (verteces[i, j].Location.X <= p.X && verteces[i, j].Location.X + Vertex.Size >= p.X
                        && verteces[i, j].Location.Y <= p.Y && verteces[i, j].Location.Y + Vertex.Size >= p.Y)
                    {
                        return this.verteces[i, j];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Puts a well in the specific vertex
        /// </summary>
        /// <param name="ver"></param>
        public void MakeWall(Vertex ver)
        {
            if (ver.Position != this.Start && ver.Position != this.End)
                this.verteces[ver.Position.Y, ver.Position.X] = new Vertex(ver.Location, ver.Position, false);
        }

        /// <summary>
        /// Removes a wall from the given vertex
        /// </summary>
        /// <param name="wall"></param>
        public void RemoveWall(Vertex wall)
        {
            this.verteces[wall.Position.Y, wall.Position.X] = new Vertex(wall.Location, wall.Position, true);
        }
        
        /// <summary>
        /// Release used resources
        /// </summary>
        public void Dispose()
        {
            this.font.Dispose();
            this.penPath.Dispose();
        }
    }
}
