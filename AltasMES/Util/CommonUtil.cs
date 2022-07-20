using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
