<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebOrder.aspx.cs" Inherits="AviaMvcApp.WebOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="Content/Site.css"/>
    <title></title>
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
                        <h1>Бронювання авіаквитка.</h1>
                    </hgroup>
                    
                </div>
            </section>
            <section class="content-wrapper main-content clear-fix">
                <br />
                <asp:Panel ID="Panel3" runat="server" Visible="False">
                    <br />
                    Ви вибрали рейc №<asp:Label ID="Label2" runat="server"></asp:Label>
                    &nbsp;за маршрутом
                    <asp:Label ID="Label3" runat="server"></asp:Label>
                    &nbsp;-
                    <asp:Label ID="Label4" runat="server"></asp:Label>
                    &nbsp;час вильоту
                    <asp:Label ID="Label5" runat="server"></asp:Label>
                    <br />
                    <br />
                    Клас<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:AviaDBConnectionString %>" SelectCommand="SELECT * FROM [Class]"></asp:SqlDataSource>
                    <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="Id">
                    </asp:DropDownList>
                    <br />
                    <br />
                    Місце<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:AviaDBConnectionString %>" SelectCommand="SELECT * FROM [Place] WHERE (([Class] = @Class) AND ([FreePlace] = @FreePlace) AND ([Flight] = @Flight))" >
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownList3" Name="Class" PropertyName="SelectedValue" Type="Int32" />
                            <asp:Parameter DefaultValue="true" Name="FreePlace" Type="Boolean" />
                            <asp:ControlParameter ControlID="Label2" Name="Flight" PropertyName="Text" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="SqlDataSource3" DataTextField="Number" DataValueField="Number" SelectCommand="SELECT DISTINCT * FROM [Place] WHERE (([Class] = @Class) AND ([FreePlace] = @FreePlace) AND ([Flight] = @Flight2))">
                    </asp:DropDownList>
                    <asp:Label ID="Label17" runat="server" Text="(*) - поле обов'язкове для замовлення" Visible="False"></asp:Label>
                    <br />
                    <br />
                    Прізвище<br />
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server" Text="(*) - поле обов'язкове для замовлення" Visible="False"></asp:Label>
                    <br />
                    Ім&#39;я<br />
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    <asp:Label ID="Label7" runat="server" Text="(*) - поле обов'язкове для замовлення" Visible="False"></asp:Label>
                    <br /> По батькові<br />
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    <asp:Label ID="Label8" runat="server" Text="(*) - поле обов'язкове для замовлення" Visible="False"></asp:Label>
                    <br /> Дата народження<br /> <br /> Рік:
                    <asp:DropDownList ID="DropDownList5" runat="server" DataSourceID="SqlDataSource4" DataTextField="Year" DataValueField="Year">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:AviaDBConnectionString %>" SelectCommand="SELECT [Year] FROM [Year] ORDER BY [Year] DESC"></asp:SqlDataSource>
                    Місяць:
                    <asp:DropDownList ID="DropDownList6" runat="server" DataSourceID="SqlDataSource5" DataTextField="Month" DataValueField="Id">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:AviaDBConnectionString %>" SelectCommand="SELECT * FROM [Month]"></asp:SqlDataSource>
                    День:
                    <asp:DropDownList ID="DropDownList7" runat="server" DataSourceID="SqlDataSource6" DataTextField="Day" DataValueField="Day">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:AviaDBConnectionString %>" SelectCommand="SELECT * FROM [Day]"></asp:SqlDataSource>
                    <asp:Label ID="Label9" runat="server" Visible="False"></asp:Label>
                    <br />
                    <div style="text-align: center"><asp:Button ID="Button5" runat="server" Text="Забронювати" OnClick="Button5_Click" />
                        <br />
                        (термін дії броні - 2 дні)</div>
                </asp:Panel>
                <asp:Panel ID="Panel6" runat="server" Visible="False">
                    <h3>Замовлення проведено успішно.<br /></h3>
                    <br />
                    Ваш рейc №<asp:Label ID="Label10" runat="server"></asp:Label>
                    &nbsp;за маршрутом
                    <asp:Label ID="Label11" runat="server"></asp:Label>
                    &nbsp;-
                    <asp:Label ID="Label12" runat="server"></asp:Label>
                    ,&nbsp;час вильоту
                    <asp:Label ID="Label13" runat="server"></asp:Label>
                    , клас -&nbsp;
                    <asp:Label ID="Label14" runat="server"></asp:Label>
                    , місце -
                    <asp:Label ID="Label15" runat="server"></asp:Label>
                    .<br /> Для того, щоб отримати квиток вам необхідно на протязі двох днів оплатити замовлення. При оплаті необхідно надати номер замовлення.<br />&nbsp;
                    <br />
&nbsp;<div style="text-align: center; font-weight: bold;">
                        Ваш номер замовлення : <b>
                        <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="14pt"></asp:Label>
                        <br />
                        </b>
                        <br />
                        <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="Продовжити" />
                    </div>
                </asp:Panel>
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
