<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ListUsers.aspx.cs" Inherits="About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        List Users
    </h2>
    <hr />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" 
        SelectCommand="SELECT [id], [first_name], [last_name], [email] FROM [UserInfo] ORDER BY [last_name], [first_name]"></asp:SqlDataSource>
    
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="id" />
            <asp:BoundField DataField="first_name" HeaderText="First Name" 
                SortExpression="first_name" />
            <asp:BoundField DataField="last_name" HeaderText="Last Name" 
                SortExpression="last_name" />
            <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
        </Columns>
    </asp:GridView>
    
</asp:Content>

