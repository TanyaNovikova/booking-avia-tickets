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
  public partial class WebFind : System.Web.UI.Page
    {
      //string flight="2";
      
      string cs = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=\"D:\\KPI\\Технології створення програмних продуктів\\AviaMvcApp\\AviaMvcApp\\App_Data\\AviaDB.mdf\";Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string r = DropDownList1.Text;
            DropDownList1.Text = DropDownList2.Text;
            DropDownList2.Text = r;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string ConnectionString = cs;
                //"Data Source=(localdb)\\Projects;Initial Catalog=AviaDB; Integrated Security=True;Connect Timeout=30;Encrypt=False";//Initial Catalog=AviaDB;
            SqlConnection conn = new SqlConnection(); 
            conn.ConnectionString = ConnectionString;
            conn.Open();
            SqlCommand myCommand = conn.CreateCommand();
            
            string date = "day([DepartureTime])=@day and month([DepartureTime])=@month and year([DepartureTime])=@year";            
            string idA1 = "(Select DISTINCT [Id] From [dbo].[Airport] where [Name]=@Airport1)";
            string idA2 = "(Select DISTINCT [Id] From [dbo].[Airport] where [Name]=@Airport2)"; 
            string existsPlaces = "(Select * From [dbo].[Place] where [Flight]=f.[Id] and [FreePlace]='true')";
            myCommand.CommandText = "Select [Id],[Airport1],[Airport2],[DepartureTime],[ArrivalTime] From [dbo].[Flight] f where [Airport1]=" + idA1 + " and [Airport2]=" + idA2 + " and " + date + " and EXISTS" + existsPlaces + ";";

            myCommand.Parameters.Add("@Airport1", SqlDbType.NVarChar, 50);
            myCommand.Parameters["@Airport1"].Value = DropDownList1.Text;
            myCommand.Parameters.Add("@Airport2", SqlDbType.NVarChar, 50);
            myCommand.Parameters["@Airport2"].Value = DropDownList2.Text;
            myCommand.Parameters.Add("@day", SqlDbType.Char);
            myCommand.Parameters["@day"].Value = Calendar1.SelectedDate.Day.ToString();
            myCommand.Parameters.Add("@month", SqlDbType.Char);
            myCommand.Parameters["@month"].Value = Calendar1.SelectedDate.Month.ToString();
            myCommand.Parameters.Add("@year", SqlDbType.Char);
            myCommand.Parameters["@year"].Value = Calendar1.SelectedDate.Year.ToString();

            Panel1.Visible = true;
            Panel4.Visible = true;

            SqlDataReader dataReader = myCommand.ExecuteReader();
            List<string> flight = new List<string>();
            List<string> date1=new List<string>();
            List<string> date2=new List<string>();
            int countFlights=0;
            while (dataReader.Read())
            {
                flight.Add(""+dataReader[0]);
                date1.Add("" + dataReader[3]);
                date2.Add("" + dataReader[4]);
                countFlights++;         
            }            
            dataReader.Close();
            if (countFlights == 0)
            {
                Panel1.Visible = false;
                Panel4.Visible = false;
                Label1.Text = "На жаль, незнайдено жодного рейсу.";
            }
            else
            {
                Label1.Text = "";
                for (int i = 0; i < countFlights; i++)
                {
                    //flight#
                    this.Panel1.Controls.Add(new LiteralControl("<tr> <td style=\"border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;\">" + flight[i] + "</td>"));
                    //plac1
                    this.Panel1.Controls.Add(new LiteralControl("<td style=\"border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;\">" + DropDownList1.Text + "</td>"));
                    //place2
                    this.Panel1.Controls.Add(new LiteralControl("<td style=\"border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;\">" + DropDownList2.Text + "</td>"));
                    //date1
                    this.Panel1.Controls.Add(new LiteralControl("<td style=\"border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;\">" + date1[i] + "</td>"));
                    //date2
                    this.Panel1.Controls.Add(new LiteralControl("<td style=\"border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;\">" + date2[i] + "</td>"));

                    //places 1 class
                    SqlCommand myCommandP1 = conn.CreateCommand();
                    myCommandP1.CommandText = "Select COUNT(*) From [dbo].[Place]  Where [Flight]=@Flight and [Class]=1 and [FreePlace]='true';";
                    myCommandP1.Parameters.Add("@Flight", SqlDbType.Int);
                    myCommandP1.Parameters["@Flight"].Value = Convert.ToInt32(flight[i]);
                    SqlDataReader dataReaderP1 = myCommandP1.ExecuteReader();
                    while (dataReaderP1.Read())
                    {
                        this.Panel1.Controls.Add(new LiteralControl("<td style=\"border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;\">" + dataReaderP1[0] + " - 1K<br/>"));
                    }
                    dataReaderP1.Close();
                    //places 2 class
                    SqlCommand myCommandP2 = conn.CreateCommand();
                    myCommandP2.CommandText = "Select COUNT(*) From [dbo].[Place]  Where [Flight]=@Flight and [Class]=2 and [FreePlace]='true';";
                    myCommandP2.Parameters.Add("@Flight", SqlDbType.Int);
                    myCommandP2.Parameters["@Flight"].Value = Convert.ToInt32(flight[i]);
                    SqlDataReader dataReaderP2 = myCommandP2.ExecuteReader();
                    while (dataReaderP2.Read())
                    {
                        this.Panel1.Controls.Add(new LiteralControl(dataReaderP2[0] + " - 2K</td>"));
                    }
                    dataReaderP2.Close();
                    //tariff 1 class
                    SqlCommand myCommandT1 = conn.CreateCommand();
                    myCommandT1.CommandText = "Select [Tariff] From [dbo].[Tariff]  Where [Flight]=@Flight and [Class]=1;";
                    myCommandT1.Parameters.Add("@Flight", SqlDbType.Int);
                    myCommandT1.Parameters["@Flight"].Value = Convert.ToInt32(flight[i]);
                    SqlDataReader dataReaderT1 = myCommandT1.ExecuteReader();
                    while (dataReaderT1.Read())
                    {
                        this.Panel1.Controls.Add(new LiteralControl("<td style=\"border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;\">" + dataReaderT1[0] + "грн. - 1K<br/>"));
                    }
                    dataReaderT1.Close();
                    //tariff 2 class
                    SqlCommand myCommandT2 = conn.CreateCommand();
                    myCommandT2.CommandText = "Select [Tariff] From [dbo].[Tariff]  Where [Flight]=@Flight and [Class]=2;";
                    myCommandT2.Parameters.Add("@Flight", SqlDbType.Int);
                    myCommandT2.Parameters["@Flight"].Value = Convert.ToInt32(flight[i]);
                    SqlDataReader dataReaderT2 = myCommandT2.ExecuteReader();
                    while (dataReaderT2.Read())
                    {
                        this.Panel1.Controls.Add(new LiteralControl(dataReaderT2[0] + "грн. - 2K</td>"));
                    }
                    dataReaderT2.Close();
                    this.Panel1.Controls.Add(new LiteralControl("<td style=\"border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF; align=\"center;\"\">"));
                    Button b = new Button();
                    b.ID = flight[i] + "";
                    b.Text = "Вибрати";
                    b.PostBackUrl = "WebOrder.aspx?Flight="+flight[i];
                    this.Panel1.Controls.Add(b);
                    this.Panel1.Controls.Add(new LiteralControl("</td></tr>"));//<a OnClick=\"MakeOrder()\">Вибрать</a>
                    //this.Panel1.Controls.Add(new Control("<asp:Button ID=\"Button3\" runat=\"server\" OnClick=\"Button2_Click\" Text=\"Знайти\" />"));
                }
                this.Panel1.Controls.Add(new LiteralControl("</table>"));
            }
            conn.Close();
        }
       /* private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
                flight = ((RadioButton)sender).ID.ToString();
        }
        private Button GetButton(string id, string name)
        {
            Button b = new Button();
            b.Text = name;
            b.ID = id;
            b.Click += new EventHandler(Button_Click);
            b.OnClientClick = "Button_Click('" + b.ClientID + "')";
            return b;
        }
        protected void Button_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), ((Button)sender).ID, "<script>alert('Button_Click');</script>");
            Response.Write(DateTime.Now.ToString() + ": " + ((Button)sender).ID + " was clicked");
            
            
            Panel1.Visible = false;
            //Panel2.Visible = true;
        }*/
        /*Response.Redirect("./course.aspx?CourseID=4") 
         
         Label.Text = "Вы выбрали курс:"+ Reqest.QueryString("CourselD")*/
       /* protected void Button3_Click(object sender, EventArgs e)
        {
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
                Label2.Text +=dataReader[0];
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
        }*/

        /*protected void Button5_Click(object sender, EventArgs e)
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
                string sND="";
                while (dataReaderND.Read())
                {
                    sND += dataReaderND[0];
                }
                dataReaderND.Close();
                int numDay = Convert.ToInt32(sND);                

                string date="";
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
                    int IdPas = Convert.ToInt32(sId)+1;

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
                    SqlDataReader dataReaderIdTic= IdCommand.ExecuteReader();
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

        }*/
    }

}
/*
 <td class="auto-style4" style="border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;"></td>
                            <td class="auto-style3" style="border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;"></td>
                            <td class="auto-style3" style="border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;"></td>
                            <td class="auto-style6" style="border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;"></td>
                            <td style="border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF; width: 100%;"></td>
 */