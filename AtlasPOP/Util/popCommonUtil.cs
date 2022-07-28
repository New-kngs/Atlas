using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AtlasDTO;

namespace AtlasPOP
{
    public class popCommonUtil
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

    }
}
