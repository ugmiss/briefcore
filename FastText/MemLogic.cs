using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KTDictSeg;
using System.IO;
using TelerikUsing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using DataAccess;

namespace FastText
{
    public class MemLogic
    {
        SqlExecuter executer = new SqlExecuter();
        public static CSimpleDictSeg m_SimpleDictSeg;
        public void Seg(Mem mem, String fulltext)
        {
            if (m_SimpleDictSeg == null)
            {
                try
                {
                    m_SimpleDictSeg = new CSimpleDictSeg();
                    m_SimpleDictSeg.DictPath = Path.Combine(Environment.CurrentDirectory, "Data") + Path.DirectorySeparatorChar; ;
                    m_SimpleDictSeg.LoadDict();
                }
                catch (Exception e1)
                {
                    m_SimpleDictSeg = null;
                    MessageBox.Show(String.Format("Load Dict Fail! ErrMsg:{0}", e1.Message),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            m_SimpleDictSeg.FilterStopWords = false;
            m_SimpleDictSeg.MatchName = true;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            ArrayList words = m_SimpleDictSeg.Segment(fulltext);
            watch.Stop();
       
            StringBuilder wordsString = new StringBuilder();
            List<string> exists = new List<string>();
            DataTable dt= executer.QueryTable("select keyword from fulltext where memid={0}", mem.Id);
            if(dt!=null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    exists.Add(dr["keyword"].ToString());  
                }

                

            }
            var q = (from c in words.ToArray() select c).Distinct();

            foreach (String str in q.ToList())
            {
                if (str.Length == 1)
                {
                    if (char.IsSymbol(str, 0) || char.IsWhiteSpace(str, 0) || char.IsControl(str, 0)||char.IsSeparator(str,0)||char.IsSurrogate(str,0)||char.IsHighSurrogate(str,0))
                        continue;
                    if(" '!\"()*,，.。/:;?@[]_{}".IndexOf(str)>=0)
                        continue;
                }
                if ((from c in exists where c == str select c).ToList().Count == 0)
                {
                    executer.NonQuery("insert into fulltext(keyword,memid) values({0},{1})", str, mem.Id);
                }
            }
        }
        public MemLogic()
        {
            executer.OpenConnection();
        }
        public void Add(Mem mem)
        {
            executer.NonQuery("insert into mem(id,assort,title,context,type,tag,files,iscommon,lastaccessdate) values({0},{1},{2},{3},{4},{5},{6},{7},{8})", mem.Id, mem.Assort, mem.Title, mem.Context, mem.Type, mem.Tag, mem.Files, mem.IsCommon, mem.LastAccessDate);
        }
        public void Del(Mem mem)
        {
            executer.NonQuery("delete from mem where id={0}", mem.Id);
        }
        public void Update(Mem mem)
        {
            executer.NonQuery("update mem set assort={0},title={1},context={2},type={3},tag={4},files={5},iscommon={6},lastaccessdate={7} where id={8}", mem.Assort, mem.Title, mem.Context, mem.Type, mem.Tag, mem.Files, mem.IsCommon, DateTime.Now, mem.Id);
        }
        public Mem[] Get()
        {
            List<Mem> menList = new List<Mem>();
            DataTable dt = executer.QueryTable("select id,assort,title,type,tag,files,iscommon,lastaccessdate from mem order by lastaccessdate desc");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Mem mem = new Mem(dr);
                    menList.Add(mem);
                }
            }
            return menList.ToArray();
        }

        public Mem[] GetByFullText(string key)
        {
            List<Mem> menList = new List<Mem>();
            DataTable dt = executer.QueryTable("select distinct id,assort,title,type,tag,iscommon,lastaccessdate from v_fulltext where (keyword like ('%'+{0}+'%') or title like ('%'+{0}+'%'))  order by lastaccessdate desc",key);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Mem mem = new Mem(dr);
                    menList.Add(mem);
                }
            }
            return menList.ToArray();
        }
        public Mem[] GetByString(string key)
        {
            List<Mem> menList = new List<Mem>();
            DataTable dt = executer.QueryTable("select id,assort,title,type,tag,files,iscommon,lastaccessdate from mem where assort like ('%'+{0}+'%')  order by lastaccessdate desc",key);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Mem mem = new Mem(dr);
                    menList.Add(mem);
                }
            }
            return menList.ToArray();
        }

        public Mem GetById(string id)
        {
            DataTable dt = executer.QueryTable("select id,assort,title,context,type,tag,files,iscommon,lastaccessdate from mem where id={0}", id);
            if (dt != null && dt.Rows.Count > 0)
            {
                Mem mem = new Mem(dt.Rows[0]);
                return mem;
            }
            return null;
        }

    }
    public class AssortInfoLogic
    {
        SqlExecuter executer = new SqlExecuter();
        public AssortInfoLogic()
        {
            executer.OpenConnection();
        }
        public void Add(AssortInfo assortInfo)
        {
            executer.NonQuery("insert into assortInfo(assortid,assorttext) values({0},{1})", assortInfo.AssortId, assortInfo.AssortText);
        }
        public void Del(AssortInfo assortInfo)
        {
            executer.NonQuery("delete from assortInfo where AssortId={0}", assortInfo.AssortId);
        }
        public void Update(AssortInfo assortInfo)
        {
            executer.NonQuery("update assortInfo set assorttext={0} where AssortId={1}", assortInfo.AssortText, assortInfo.AssortId);
        }
        public AssortInfo[] Get()
        {
            List<AssortInfo> assortInfoList = new List<AssortInfo>();
            DataTable dt = executer.QueryTable("select AssortId,AssortText from assortInfo ");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AssortInfo assortInfo = new AssortInfo(dr);
                    assortInfoList.Add(assortInfo);
                }
            }
            return assortInfoList.ToArray();
        }
        public AssortInfo GetByText(string text)
        {
            DataTable dt = executer.QueryTable("select assortid,assorttext from assortInfo where assorttext={0}", text);
            if (dt != null && dt.Rows.Count > 0)
            {
                AssortInfo assortInfo = new AssortInfo(dt.Rows[0]);
                return assortInfo;
            }
            return null;
        }
    }
}
