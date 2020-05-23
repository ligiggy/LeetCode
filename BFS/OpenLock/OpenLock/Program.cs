using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLock
{
    class Program
    {


        private static int nDepth = 0;

        private static string[] strDeadArray = null;

        private static string strTarget = string.Empty;

        private static List<string> strAllList = new List<string>();

        static void Main(string[] args)
        {

            string strDead = Console.ReadLine();

            strTarget = Console.ReadLine();

            strDeadArray = strDead.Split(',');

            List<string> originList = new List<string>();

            originList.Add("0000");

            strAllList.Add("0000");

            FindTarget(originList);

            Console.WriteLine(nDepth);

        }


        private static void FindTarget(List<string> strOriginList)
        {
            List<string> strList = new List<string>();

            for (int i = 0; i < strOriginList.Count; i++)
            {
                if (strDeadArray.Contains(strOriginList[i]))
                {
                    continue;
                }

                if(strOriginList[i] == strTarget)
                {
                    return;
                }

                List<string> strNodeList = GetNextLockArray(strOriginList[i]);

                for (int j = 0; j < strNodeList.Count; j++)
                {
                    if (strAllList.Contains(strNodeList[j]))
                    {
                        continue;
                    }

                    strList.Add(strNodeList[j]);

                    strAllList.Add(strNodeList[j]);
                }
            }

            if(strList.Count ==0)
            {
                nDepth = -1;
                return;
            }

            nDepth++;

            FindTarget(strList);

            if (strList.Contains("9999"))
            {
                nDepth = -1;
                return;
            }
        }


        //获取当前字符串的子节点
        private static List<string> GetNextLockArray(string strCurrentPassword)
        {

            List<string> strList = new List<string>();

            char[] charArray = strCurrentPassword.ToCharArray();

            List<string> strOriginList = new List<string>();


            for (int i = 0; i < charArray.Length; i++)
            {
                strOriginList.Add(charArray[i].ToString());
            }

            List<string> strCopyList = new List<string>();

            for (int i = 0; i < strOriginList.Count; i++)
            {
                strCopyList.Add(strOriginList[i]);
            }

            for (int i = 0; i < strOriginList.Count; i++)
            {
                int n = Convert.ToInt32(strOriginList[i]);

                //+1的部分
                if (n + 1 == 10)
                {
                    strOriginList[i] = "0";
                }
                else
                {
                    strOriginList[i] = (n + 1).ToString();
                }

                string str = string.Empty;
                for (int j = 0; j < strOriginList.Count; j++)
                {
                    str += strOriginList[j];
                }

                strList.Add(str);

                //-1的部分
                if (n - 1 == -1)
                {
                    strOriginList[i] = "9";
                }
                else
                {
                    strOriginList[i] = (n - 1).ToString();
                }

                str = string.Empty;
                for (int j = 0; j < strOriginList.Count; j++)
                {
                    str += strOriginList[j];
                }

                strList.Add(str);

                for (int j = 0; j < strOriginList.Count; j++)           //重置strOriginList
                {
                    strOriginList[j] = strCopyList[j];
                }

            }


            return strList;

        }



    }
}
