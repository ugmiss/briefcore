// Upload to codeproject.com
// By Ibraheem AlKilanny
// d3_ib@hotmail.com - http://sites.google.com/site/ibraheemalkilany/
// all rights reserved 2011

using System.Drawing;

namespace Path_Finding
{
    /// <summary>
    /// Represents a graph node
    /// </summary>
    internal class Vertex
    {
        /// <summary>
        /// Initializes new instance of Vertex
        /// </summary>
        /// <param name="location">The vertex location on the graphics surface</param>
        /// <param name="position">The vertex location on the 2d container</param>
        /// <param name="walkable">Indicates whether the vertex can be visited (false if the vertex can not be)</param>
        public Vertex(Point location, Point position, bool walkable)
        {
            Location = location;
            this.Position = position;
            this.Walkable = walkable;
            this.Adjacents = new Point[8]
            {
                new Point(this.Position.X + 1, this.Position.Y),
                new Point(this.Position.X + 1, this.Position.Y - 1),
                new Point(this.Position.X, this.Position.Y - 1),
                new Point(this.Position.X - 1, this.Position.Y - 1),
                new Point(this.Position.X - 1, this.Position.Y),
                new Point(this.Position.X - 1, this.Position.Y + 1),
                new Point(this.Position.X, this.Position.Y + 1),
                new Point(this.Position.X + 1, this.Position.Y + 1)
            };
        }

        /// <summary>
        /// Indicates whether the vertex can be visited (false if the vertex can not be)
        /// </summary>
        public readonly bool Walkable;

        /// <summary>
        /// The vertex location on the two-dimensional container
        /// </summary>
        public Point Position
        {
            get;
            private set;
        }

        /// <summary>
        /// The vertex location on the graphics surface
        /// </summary>
        public Point Location
        {
            get;
            private set;
        }

        /// <summary>
        /// The vertex height and width on the graphics surface
        /// </summary>
        public static int Size = 30;

        /// <summary>
        /// Indicate the vertex statuses through the path finding
        /// </summary>
        public enum Statuses
        {
            /// <summary>
            /// The vertex is in opened list (discovered but not finished)
            /// </summary>
            OpenList,
            /// <summary>
            /// The vertex is in closed list (discovered and finished)
            /// </summary>
            ClosedList,
            /// <summary>
            /// The vertex has not been visited yet (not discovered)
            /// </summary>
            Unvisited
        }

        /// <summary>
        /// Gets an array of the vertex's adjacent vertices
        /// </summary>
        public readonly Point[] Adjacents;

        /// <summary>
        /// Gets or sets a vertex status from start to end direction
        /// </summary>
        public Statuses Status1 = Statuses.Unvisited;

        /// <summary>
        /// Gets or sets a vertex status from end to start direction
        /// </summary>
        public Statuses Status2 = Statuses.Unvisited;

        /// <summary>
        /// Gets or sets a value whether the vertex is being searched from start to end
        /// </summary>
        public bool StartToEnd;

        /// <summary>
        /// Gets or sets the vertex parent in the path finding (from start to end)
        /// </summary>
        public Vertex Parent1;

        /// <summary>
        /// Gets or sets the vertex parent in the path finding (from end to start)
        /// </summary>
        public Vertex Parent2;

        /// <summary>
        /// Gets or sets the vertex cost G (from start to end)
        /// </summary>
        public int G1;

        /// <summary>
        /// Gets or sets the vertex cost G (from end to start)
        /// </summary>
        public int G2;

        /// <summary>
        /// Gets or sets the H vertex cost (from start to end)
        /// </summary>
        public int H1;

        /// <summary>
        /// Gets or sets the H vertex cost (from end to start)
        /// </summary>
        public int H2;

        /// <summary>
        /// Gets the F cost for the vertex (from start to end)
        /// </summary>
        public int F1
        {
            get { return this.G1 + this.H1; }
        }

        /// <summary>
        /// Gets the F cost for the vertex (from end to start)
        /// </summary>
        public int F2
        {
            get { return this.G2 + this.H2; }
        }
    }
}
