<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFind.aspx.cs" Inherits="AviaMvcApp.WebFind" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="Content/Site.css"/>
    <title></title>
    <style type="text/css">
        .auto-style4 {
            width: 15%;
        }
        .auto-style10 {
            width: 10%;
        }
        .auto-style13 {
            width: 25%;
        }
        .auto-style14 {
            width: 8%;
        }
        .auto-style15 {
            width: 13%;
        }
        </style>
</head>

<body>   
    <header>
        <div class="content-wrapper">
            <div class="float-left">
                <p class="site-title">Авіаквитки</p>
            </div>
            <div class="float-right">
                <section id="login">
                      
                </section>
                <nav>
                    <ul id="menu">
                        <li></li>
                    </ul>
                </nav>
            </div>
        </div>
    </header>
    <div id="body">
        <form id="form1" runat="server">
            <section class="featured">
                <div class="content-wrapper">
                    <hgroup class="title">
                        <h1>Пошук авіаквитків.</h1>
                    </hgroup>
                    <div class="mDDL">
                        <div>Звідки:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Куди:</div>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="45%" Height="35px" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AviaDBConnectionString %>" SelectCommand="SELECT DISTINCT [Name] FROM [Airport] ORDER BY [Name]"></asp:SqlDataSource>
&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" Text="⇄" OnClick="Button1_Click" />
                        &nbsp;&nbsp;
                        <asp:DropDownList ID="DropDownList2" runat="server" Width="45%" Height="35px" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name">
                        </asp:DropDownList>
                        <br />
                        Дата вильоту:<asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#308BAD" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#26598C" Height="200px" Width="220px" SelectedDate="05/26/2015 18:57:59">
                            <DayHeaderStyle BackColor="#99BDCC" ForeColor="#336666" Height="1px"></DayHeaderStyle>

                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF"></NextPrevStyle>

                            <OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>

                            <SelectedDayStyle BackColor="#79AAD7" Font-Bold="True" ForeColor="#CCFF99"></SelectedDayStyle>

                            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666"></SelectorStyle>

                            <TitleStyle BackColor="#3394B9" BorderColor="#7AC0DA" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px"></TitleStyle>

                            <TodayDayStyle BackColor="#99CCCC" ForeColor="White"></TodayDayStyle>

                            <WeekendDayStyle BackColor="#CCCCFF"></WeekendDayStyle>
                        </asp:Calendar>    
                        <div style="position: relative; float: right;">
                            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Знайти" />
                        </div>
                        <br />

                    </div>
                </div>
            </section>
            <section class="content-wrapper main-content clear-fix">
                <br />
                <div style="position: relative; float: left">
                    <asp:Panel ID="Panel2" runat="server">
                    </asp:Panel>
                </div>
                <div style="position: relative; float: left; top: 0px; left: 0px;">
                    <asp:Panel ID="Panel1" runat="server" Visible="False">
                        <h3>Знайдені рейси</h3>
                        <table class="flight" border='1' style="border: 1px solid #808080">
                            <tr>
                                <th class="auto-style14" style="border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;">Номер рейсу</th>
                                <th class="auto-style13" style="border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;">Пункт вильоту</th>
                                <th class="auto-style13" style="border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;">Пункт призначенн</th>
                                <th class="auto-style4" style="border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;">Час вильту</th>
                                <th style="border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;" class="auto-style15">Час призначення</th>
                                <th style="border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;">Кількість місць</th>
                                <th class="auto-style10" style="border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;">Ціна</th>
                                <th style="border: 1px solid #808080; padding: 3px; border-collapse: collapse; empty-cells: hide; background-color: #FFFFFF;">Вибрать</th>
                            </tr>
                        <br />
                           
                    </asp:Panel>
                     <div style="text-align: center">
                                <asp:Panel ID="Panel4" runat="server" Visible="False">
                                    <div style="text-align: center">
                                        <asp:Panel ID="Panel5" runat="server">
                                        </asp:Panel>
                                    </div>
                                    <br />
                                </asp:Panel>
                        
                    </div>
                </div>
                <h3>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </h3>

                <h3>Інструкція по замовленню авіаквитків:                    
                </h3>
                <ol class="round">
                    <li class="one">
                        <h5>Знайдіть всі можливі рейси</h5>
                         Для пошуку рейсів вам необхідно задати пункт вильоту і пункт признаення, після цього оберіть бажану дату.
                    </li>

                    <li class="two">
                        <h5>Виберіть потрібний рейс</h5>
                        В таблиці рейсів ви можете переглянути необхідну інформацію про рейси і обрати потрібний вам.
                    </li>

                    <li class="three">
                        <h5>Зробіть замовлення</h5>
                        Ви можете легко зробити замовлення квитка.
                        </li>
                </ol>
            </section>
        </form>
    </div>
        <footer>
            <div class="content-wrapper">
                <div class="float-left">
                    <p>&copy; 2015 — приложение ASP.NET MVC</p>
                </div>
            </div>
        </footer>
</body>
</html>
