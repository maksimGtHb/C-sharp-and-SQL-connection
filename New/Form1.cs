using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace New
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           //запуск базы данных и ее запросов
           LoadData();
        }

        private void LoadData()
        {
          //строка подключения
          string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksc\source\repos\New\New\Database1.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
               
                //SQL запрос на отображение данных
                string query = @"
                    SELECT persons.first_name, persons.second_name, persons.last_name, 
                           status.name AS status_name, 
                           deps.name AS deps_name, 
                           posts.name AS posts_name, 
                           persons.date_employ, 
                           persons.date_uneploy
                    FROM persons 
                    INNER JOIN status ON persons.id=status.id 
                    INNER JOIN deps ON persons.id=deps.id 
                    INNER JOIN posts ON persons.id=posts.id;";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
             //подключение табличного поля к SQL

                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dataGridView3.DataSource = dataTable;
            }

            // Цикл по сортировки столбцов
            foreach (DataGridViewColumn column in dataGridView3.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            // Обработчик события для сортировки
            dataGridView3.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(dataGridView3_ColumnHeaderMouseClick);
        }
        //создание метода, при нажатии мыши на столбец
        private void dataGridView3_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dataGridView3.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dataGridView3.SortedColumn;
            ListSortDirection direction;

            // Если столбец не отсортирован, сортировка по возрастанию
            if (oldColumn != null)
            {
                if (oldColumn == newColumn && dataGridView3.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                {
                    direction = ListSortDirection.Ascending;
                }
                else
                {
                    direction = ListSortDirection.Descending;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            

            // Выполнение метода
            dataGridView3.Sort(newColumn, direction);
        }

        private void label2_SizeChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}




/* private void LoadData ()
 {
     try
     {
         sqlAdapter = new SqlDataAdapter("SELECT first_name, second_name, last_name, status.name," +
             " deps.name, posts.name, date_employ, date_uneploy  " +
             "FROM persons " +
             "INNER JOIN status on persons.id=status.id " +
             "INNER JOIN deps on persons.id=deps.id " +
             "INNER JOIN posts on persons.id=posts.id;", con); 

         //", persons, posts, status", con);
         sqlBuilder = new SqlCommandBuilder(sqlAdapter);

         dataSet = new DataSet();
         DataTable dataTable = new DataTable();
         sqlAdapter.Fill(dataTable);
         dataGridView2.DataSource = dataTable;

         dataGridView2.DataSource = dataSet.Tables["persons"];

     }

     catch (Exception ex)
     { 
         MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
     }
 }

 private void Form1_Load(object sender, EventArgs e)
 {
     con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksc\source\repos\New\New\Database1.mdf;Integrated Security=True");
     con.Open();

 }
}
}
*/