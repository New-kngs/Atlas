using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace AltasMES
{
    public class ExcelUtil
    {


        [DllImport("user32.dll")]


        public static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public bool ExportList<T>(List<T> dataList, string fileName, string[] importColumn, string[] ColumnName)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();
            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);


            try
            {

                //속성명을 엑셀파일의 첫번째 행에 제목을 찍고 VO Name -> GridName 으로

                int c = 0;
                foreach (PropertyInfo prop in typeof(T).GetProperties())
                {
                    foreach (string import in importColumn)
                    {
                        if (import.Equals(prop.Name))
                        {

                            xlWorkSheet.Cells[1, c + 1] = prop.Name.Replace(prop.Name.ToString(), ColumnName[c]);
                            c++;
                        }

                    }

                }


                //데이터를 찍어준다. //기존에는 Null를 생략
                for (int r = 0; r < dataList.Count; r++)
                {
                    c = 0;
                    foreach (PropertyInfo prop in typeof(T).GetProperties())
                    {
                        foreach (string import in importColumn)
                        {
                            if (import.Equals(prop.Name))
                            {

                                if (prop.GetValue(dataList[r], null) != null)
                                    xlWorkSheet.Cells[r + 2, c + 1] = prop.GetValue(dataList[r]).ToString();
                                else
                                    xlWorkSheet.Cells[r + 2, c + 1] = "";
                                c++;
                            }

                        }

                    }
                }

                //엑셀컬럼의 너비가 데이터길이에 따라서 자동 조정
                xlWorkSheet.Columns.AutoFit();


                //xls 확장자로 저장하는 경우
                xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal);

                //xlsx 확장자로 저장하는 경우
                //xlWorkBook.SaveCopyAs(dlg.FileName);
                //xlWorkBook.Saved = true;

                xlWorkBook.Close();
                xlApp.Quit();

                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message); 
                return false;
            }
            finally
            {
                //Background 안죽어서 추가
                uint processId;
                GetWindowThreadProcessId(new IntPtr(xlApp.Hwnd), out processId);
                Process p = Process.GetProcessById((int)processId);
                p.Kill();

                //releaseObject(xlWorkSheet);
                //releaseObject(xlWorkBook);
                //releaseObject(xlApp);
            }
        }

        public bool ExportDataTable(DataTable dt, string fileName)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();
            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);

            try
            {
                //컬럼명을 엑셀파일의 첫번째 행에 제목으로 찍어준다.
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    xlWorkSheet.Cells[1, c + 1] = dt.Columns[c].ColumnName;
                }

                //데이터를 찍어준다.
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        xlWorkSheet.Cells[r + 2, c + 1] = dt.Rows[r][c].ToString();
                    }
                }

                //xls 확장자로 저장하는 경우
                xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal);

                //xlsx 확장자로 저장하는 경우
                //xlWorkBook.SaveCopyAs(dlg.FileName);
                //xlWorkBook.Saved = true;

                xlWorkBook.Close();
                xlApp.Quit();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
            }
        }

        public void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public DataTable ImportDataTable(string filename)
        {
            try
            {
                string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'"; //*.xls
                string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'"; //*.xlsx

                //string.Format("안녕 {0}님 {1}살", "홍길동", 25);
                string ext = Path.GetExtension(filename);
                string connStr = string.Empty;
                if (ext == ".xls")
                    connStr = string.Format(Excel03ConString, filename, "Yes");
                else
                    connStr = string.Format(Excel07ConString, filename, "Yes");

                OleDbConnection conn = new OleDbConnection(connStr);
                conn.Open();
                DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = dtSchema.Rows[0]["TABLE_NAME"].ToString();

                string sql = $"select * from [{sheetName}]";
                OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();

                return dt;
            }
            catch
            {
                return null;
            }
        }


        public bool ExportExcelGridView(string filename , DataGridView data , string[] inputColumn)
        {


            Excel.Application excel = new Excel.Application();
            Excel.Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = workbook.Worksheets.get_Item(1);
    


            // Creating a Excel object.
            try
            {
               

                //DataGridView에 불러온 Data가 아무것도 없을 경우
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show("출력할 데이터가 없습니다.", "엑셀", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                //worksheet = workbook.ActiveSheet;
               // worksheet = (Excel.Worksheet)workbook.ActiveSheet;

                int cellRowIndex = 1;
                int cellColumnIndex = 1;



                for (int col = 0; col < data.Columns.Count; col++)
                {
                    foreach (string input in inputColumn)
                    {
                        if (input.Equals(data.Columns[col].HeaderText))
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = data.Columns[col].HeaderText;
                            cellColumnIndex++;
                        }
                    }
                }

                cellColumnIndex = 1;
                cellRowIndex++;
                for (int row = 0; row < data.Rows.Count; row++)
                {
                    for (int col = 0; col < data.Columns.Count; col++)
                    {

                        foreach (string input in inputColumn)
                        {
                            if (input.Equals(data.Columns[col].HeaderText))
                            { 
                                if (data.Rows[row].Cells[col].Value == null)
                                {
                                    worksheet.Cells[cellRowIndex, cellColumnIndex] = "";
                                    cellColumnIndex++;
                                }
                                else
                                {
                                    worksheet.Cells[cellRowIndex, cellColumnIndex] = data.Rows[row].Cells[col].Value.ToString();
                                    cellColumnIndex++;
                                }
                            }
                        }
                        
                    }
                       
                    cellColumnIndex = 1;
                    cellRowIndex++;
                }

                worksheet.Columns.AutoFit();
                workbook.SaveAs(filename, Excel.XlFileFormat.xlWorkbookNormal);
                workbook.Close();
                excel.Quit();
               

                return true;


             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"엑셀", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            finally
            {
                uint processId;
                GetWindowThreadProcessId(new IntPtr(excel.Hwnd), out processId);
                Process p = Process.GetProcessById((int)processId);
                p.Kill();

            }

        }
    }
  
}
