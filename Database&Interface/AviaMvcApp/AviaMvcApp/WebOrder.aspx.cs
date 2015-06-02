using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Data.OleDb;

namespace AviaMvcApp
{
    public partial class WebOrder : System.Web.UI.Page
    {
        string flight;
        string ticket;
        string cs = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=\"D:\\KPI\\Технології створення програмних продуктів\\AviaMvcApp\\AviaMvcApp\\App_Data\\AviaDB.mdf\";Integrated Security=True";
 
        protected void Page_Load(object sender, EventArgs e)
        {
            flight = Request.QueryString.Get(0);
            //-------------------------------------------lbl

            Panel3.Visible = true;
            Label2.Text = "";
            Label3.Text = "";
            Label4.Text = "";
            Label5.Text = "";
            string ConnectionString = cs;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            SqlCommand myCommand = conn.CreateCommand();
            myCommand.CommandText = "Select Distinct [Id],[Airport1],[Airport2],[DepartureTime] From [dbo].[Flight] where [Id]=@flight;";

            myCommand.Parameters.Add("@flight", SqlDbType.Int);
            myCommand.Parameters["@flight"].Value = flight;//----------------------

            //Panel1.Visible = false;

            SqlDataReader dataReader = myCommand.ExecuteReader();
            string a1 = "", a2 = "";
            while (dataReader.Read())
            {
                Label2.Text += dataReader[0];
                a1 += dataReader[1];
                a2 += dataReader[2];
                Label5.Text += dataReader[3];
            }
            dataReader.Close();
            //------------------A1
            SqlCommand myCommandA1 = conn.CreateCommand();
            myCommandA1.CommandText = "Select Distinct [Name] From [dbo].[Airport] where [Id]=@flight;";
            myCommandA1.Parameters.Add("@flight", SqlDbType.Int);
            myCommandA1.Parameters["@flight"].Value = a1;
            SqlDataReader dataReaderA1 = myCommandA1.ExecuteReader();
            while (dataReaderA1.Read())
            {
                Label3.Text += dataReaderA1[0];
            }
            dataReaderA1.Close();
            //------------------A2
            SqlCommand myCommandA2 = conn.CreateCommand();
            myCommandA2.CommandText = "Select Distinct [Name] From [dbo].[Airport] where [Id]=@flight;";
            myCommandA2.Parameters.Add("@flight", SqlDbType.Int);
            myCommandA2.Parameters["@flight"].Value = a2;
            SqlDataReader dataReaderA2 = myCommandA2.ExecuteReader();
            while (dataReaderA2.Read())
            {
                Label4.Text += dataReaderA2[0];
            }
            dataReaderA2.Close();
            conn.Close();
            //Label1.Text = "Здравствуйте, " + Request.QueryString.Get(0);    
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Label6.Visible = false;
            Label7.Visible = false;
            Label8.Visible = false;
            Label17.Visible = false;
            if (TextBox1.Text == "")
            {
                Label6.Visible = true;
            }
            if (TextBox2.Text == "")
            {
                Label7.Visible = true;
            }
            if (TextBox3.Text == "")
            {
                Label8.Visible = true;
            }
            if (DropDownList4.SelectedValue == "")
            {
                Label17.Visible = true;
            }
            if ((TextBox1.Text != "") && (TextBox2.Text != "") && (TextBox3.Text != "") && (DropDownList4.SelectedValue != ""))
            {
                string ConnectionString = cs;

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConnectionString;
                conn.Open();

                SqlCommand NDCommand = conn.CreateCommand();
                NDCommand.CommandText = "Select distinct [NumDay] from [dbo].[Month] where [Id]=@Id;";
                NDCommand.Parameters.Add("@Id", SqlDbType.Int);
                NDCommand.Parameters["@Id"].Value = DropDownList6.SelectedValue;
                SqlDataReader dataReaderND = NDCommand.ExecuteReader();
                string sND = "";
                while (dataReaderND.Read())
                {
                    sND += dataReaderND[0];
                }
                dataReaderND.Close();
                int numDay = Convert.ToInt32(sND);

                string date = "";
                if (Convert.ToInt32(Convert.ToString(DropDownList7.SelectedValue)) <= numDay)
                {
                    SqlCommand IdCommand = conn.CreateCommand();
                    IdCommand.CommandText = "Select Max([Id]) From [dbo].[Passenger];";
                    SqlDataReader dataReaderId = IdCommand.ExecuteReader();
                    string sId = "";
                    while (dataReaderId.Read())
                    {
                        sId += dataReaderId[0];
                    }
                    dataReaderId.Close();
                    int IdPas = Convert.ToInt32(sId) + 1;

                    date = DropDownList7.SelectedValue + "." + DropDownList6.SelectedValue + "." + DropDownList5.SelectedValue;
                    SqlCommand PasInfCommand = conn.CreateCommand();
                    PasInfCommand.CommandText = "Insert into [dbo].[Passenger]([Id],[FirstName],[LastName],[Patronymic],[DOB]) Values(@Id, @FN, @LN, @P, @DOB);";
                    PasInfCommand.Parameters.Add("@Id", SqlDbType.Int);
                    PasInfCommand.Parameters["@Id"].Value = IdPas;
                    PasInfCommand.Parameters.Add("@FN", SqlDbType.NVarChar, 50);
                    PasInfCommand.Parameters["@FN"].Value = TextBox1.Text;
                    PasInfCommand.Parameters.Add("@LN", SqlDbType.NVarChar, 50);
                    PasInfCommand.Parameters["@LN"].Value = TextBox2.Text;
                    PasInfCommand.Parameters.Add("@P", SqlDbType.NVarChar, 50);
                    PasInfCommand.Parameters["@P"].Value = TextBox3.Text;
                    PasInfCommand.Parameters.Add("@DOB", SqlDbType.Date);
                    PasInfCommand.Parameters["@DOB"].Value = date;
                    PasInfCommand.ExecuteNonQuery();

                    IdCommand.CommandText = "Select Max([Id]) From [dbo].[Ticket];";
                    SqlDataReader dataReaderIdTic = IdCommand.ExecuteReader();
                    sId = "";
                    while (dataReaderIdTic.Read())
                    {
                        sId += dataReaderIdTic[0];
                    }
                    dataReaderIdTic.Close();
                    ticket = (Convert.ToInt32(sId) + 1).ToString();
                    SqlCommand TicInfCommand = conn.CreateCommand();

                    TicInfCommand.CommandText = "Insert into [dbo].[Ticket] values((Select Max([Id]) From [dbo].[Ticket])+1,@flight,@IdPlace,@IdPassenger,2,'false',@IdClass, @DateOrder);";//,'01.06.2015');";
                    TicInfCommand.Parameters.Add("@flight", SqlDbType.Int);
                    TicInfCommand.Parameters["@flight"].Value = Label2.Text;
                    TicInfCommand.Parameters.Add("@IdPlace", SqlDbType.Int);
                    TicInfCommand.Parameters["@IdPlace"].Value = DropDownList4.SelectedValue;
                    TicInfCommand.Parameters.Add("@IdPassenger", SqlDbType.Int);
                    TicInfCommand.Parameters["@IdPassenger"].Value = IdPas;
                    TicInfCommand.Parameters.Add("@IdClass", SqlDbType.Int);
                    TicInfCommand.Parameters["@IdClass"].Value = DropDownList3.SelectedValue;
                    TicInfCommand.Parameters.Add("@DateOrder", SqlDbType.Date);
                    //date = DateTime.Today.Day + "." + DateTime.Today.Month + "." + DateTime.Today.Year;
                    TicInfCommand.Parameters["@DateOrder"].Value = DateTime.Today;
                    TicInfCommand.ExecuteNonQuery();

                    SqlCommand UpdPlaceCommand = conn.CreateCommand();
                    UpdPlaceCommand.CommandText = "Update [dbo].[Place] Set [FreePlace] = 'false' Where [flight] = @flight and [Id]=@IdPlace;";
                    UpdPlaceCommand.Parameters.Add("@flight", SqlDbType.Int);
                    UpdPlaceCommand.Parameters["@flight"].Value = Label2.Text;
                    UpdPlaceCommand.Parameters.Add("@IdPlace", SqlDbType.Int);
                    UpdPlaceCommand.Parameters["@IdPlace"].Value = DropDownList4.SelectedValue;
                    UpdPlaceCommand.ExecuteNonQuery();
                }
                else
                {
                    Label9.Visible = true;
                    Label9.Text = "У вибраному вами місяці " + numDay.ToString() + "днів!";
                }
                Panel3.Visible = false;
                Panel6.Visible = true;
                //---------------------------------------
                SqlCommand myCommand = conn.CreateCommand();

                myCommand.CommandText = "Select Distinct [Id],[Airport1],[Airport2],[DepartureTime] From [dbo].[Flight] where [Id]=@flight;";

                myCommand.Parameters.Add("@flight", SqlDbType.Int);
                myCommand.Parameters["@flight"].Value = flight;//----------------------

                //Panel1.Visible = false;

                SqlDataReader dataReader = myCommand.ExecuteReader();
                string a1 = "", a2 = "";
                while (dataReader.Read())
                {
                    Label10.Text += dataReader[0];
                    a1 += dataReader[1];
                    a2 += dataReader[2];
                    Label13.Text += dataReader[3];
                }
                dataReader.Close();
                //------------------A1
                SqlCommand myCommandA1 = conn.CreateCommand();
                myCommandA1.CommandText = "Select Distinct [Name] From [dbo].[Airport] where [Id]=@flight;";
                myCommandA1.Parameters.Add("@flight", SqlDbType.Int);
                myCommandA1.Parameters["@flight"].Value = a1;
                SqlDataReader dataReaderA1 = myCommandA1.ExecuteReader();
                while (dataReaderA1.Read())
                {
                    Label11.Text += dataReaderA1[0];
                }
                dataReaderA1.Close();
                //------------------A2
                SqlCommand myCommandA2 = conn.CreateCommand();
                myCommandA2.CommandText = "Select Distinct [Name] From [dbo].[Airport] where [Id]=@flight;";
                myCommandA2.Parameters.Add("@flight", SqlDbType.Int);
                myCommandA2.Parameters["@flight"].Value = a2;
                SqlDataReader dataReaderA2 = myCommandA2.ExecuteReader();
                while (dataReaderA2.Read())
                {
                    Label12.Text += dataReaderA2[0];
                }
                dataReaderA2.Close();
                //------------------Class
                SqlCommand myCommandClass = conn.CreateCommand();
                myCommandClass.CommandText = "Select Distinct [IdClass] From [dbo].[Ticket] where [Id]=@Id;";
                myCommandClass.Parameters.Add("@Id", SqlDbType.Int);
                myCommandClass.Parameters["@Id"].Value = ticket;
                SqlDataReader dataReaderClass = myCommandClass.ExecuteReader();
                while (dataReaderClass.Read())
                {
                    Label14.Text += dataReaderClass[0];
                }
                dataReaderClass.Close();
                //------------------Place
                SqlCommand myCommandPlace = conn.CreateCommand();
                myCommandPlace.CommandText = "Select Distinct [IdClass] From [dbo].[Ticket] where [Id]=@Id;";
                myCommandPlace.Parameters.Add("@Id", SqlDbType.Int);
                myCommandPlace.Parameters["@Id"].Value = ticket;
                SqlDataReader dataReaderPlace = myCommandPlace.ExecuteReader();
                while (dataReaderPlace.Read())
                {
                    Label15.Text += dataReaderPlace[0];
                }
                dataReaderPlace.Close();
                Label16.Text = ticket.ToString();
                conn.Close();
            }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Panel6.Visible = false;
            Response.Redirect("./WebFind.aspx?A1=");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("./WebFind.aspx");//-------------------------------------ToFindPage
        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList4.DataSourceID = "SqlDataSource3";
            //DropDownList4.SelectCommand="SELECT DISTINCT * FROM [Place] WHERE (([Class] = @Class) AND ([FreePlace] = @FreePlace) AND ([Flight] = @Flight2))"
        }
    }
}