using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AtlasDTO;

namespace AltasMES
{
    public class CommonUtil
    {
        public static void ComboBinding(ComboBox cbo, List<ComboItemVO> src, string category, bool blankItem = true, string blankText = "")
        {
            //var list = (from item in src
            //            where item.Category.Contains(category)
            //            select item).ToList();

            var list = src.Where<ComboItemVO>((e) => e.Category.Equals(category)).ToList();

            if (blankItem)
            {
                ComboItemVO newItem = new ComboItemVO();
                newItem.Code = "";
                newItem.CodeName = blankText;
                newItem.Category = category;

                list.Insert(0, new ComboItemVO
                { Code = "", CodeName = blankText, Category = category }
                );
            }

            cbo.DisplayMember = "CodeName";
            cbo.ValueMember = "Code";
            cbo.DataSource = list;
        }


        public static void ComboBinding<T>(ComboBox cbo, List<T> src, string dispalyField, string valueField, bool blankItem = true, string blankText = "")
        {
            //var list = (from item in src
            //            where item.Category.Contains(category)
            //            select item).ToList();

            if (blankItem)
            {
                T newItem = default(T);
                newItem = Activator.CreateInstance<T>();
                PropertyInfo prop;
                prop = newItem.GetType().GetProperty(valueField);
                if (prop != null)
                    prop.SetValue(newItem, "", null);

                prop = newItem.GetType().GetProperty(dispalyField);
                if (prop != null)
                    prop.SetValue(newItem, blankText, null);

                src.Insert(0, newItem);
            }

            cbo.ValueMember = valueField;
            cbo.DisplayMember = dispalyField;
            cbo.DataSource = src;
        }



        /// <summary>
        /// 김준모/콤보박스 바인딩(조건 : 리스트{화면표시값, 벨류값} 필수) 
        /// display Colum의 type이 string이어야한다.
        /// </summary>
        /// <typeparam name="T">해당VO</typeparam>
        /// <param name="cbo">콤보박스</param>
        /// <param name="list">바인딩 할 List</param>
        /// <param name="dis">화면표시, 블랭크추가시 prop명</param>
        /// <param name="val">cbo벨류값</param>
        /// <param name="blank">콤보박스 블랭크 유무 토글</param>
        /// <param name="blankText">콤보박스 블랭크 텍스트란</param>       
        public static bool ComboBinding1<T>(ComboBox cbo, List<T> list, string dis, string val, bool blank = false, string blankText = "전체") where T : class
        {
            try
            {
                if (blank)
                {
                    T obj = default(T);

                    obj = Activator.CreateInstance<T>();
                    obj.GetType().GetProperty(dis).SetValue(obj, blankText);

                    list.Insert(0, obj);
                }
                cbo.DataSource = null;
                cbo.DropDownStyle = ComboBoxStyle.DropDownList;
                cbo.DisplayMember = dis;
                cbo.ValueMember = val;

                cbo.DataSource = list;
                return true;
            }
            catch
            {
                return false;
            }
        }

        // List<VO> => DataTable 
        public static DataTable LinqQueryToDataTable(IEnumerable<dynamic> v)
        {
            //We really want to know if there is any data at all
            var firstRecord = v.FirstOrDefault();
            if (firstRecord == null)
                return null;

            //So dear record, what do you have?
            PropertyInfo[] infos = firstRecord.GetType().GetProperties();
            //Our table should have the columns to support the properties
            DataTable table = new DataTable();
            //Add, add, add the columns
            foreach (var info in infos)
            {
                Type propType = info.PropertyType;
                //Nullable types should be handled too
                if (propType.IsGenericType
                    && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    table.Columns.Add(info.Name, Nullable.GetUnderlyingType(propType));
                }
                else
                {
                    table.Columns.Add(info.Name, info.PropertyType);
                }
            }

            DataRow row;
            foreach (var record in v)
            {
                row = table.NewRow();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row[i] = infos[i].GetValue(record) != null ?
                    infos[i].GetValue(record) : DBNull.Value;
                }
                table.Rows.Add(row);
            }

            //Table is ready to serve.
            table.AcceptChanges();
            return table;
        }

    }
}
