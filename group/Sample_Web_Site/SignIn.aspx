<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SignIn.aspx.cs" Inherits="SignIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Sign-In</h2>
    <hr />
    <table>
        <tr>
            <td>
                <asp:Label ID="lblEmail" runat="server" Text="Email: "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbxEmail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="requiredFieldValidator3" runat="server" ErrorMessage="* Required Field" ControlToValidate="tbxEmail" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblPassword" runat="server" Text="Password: "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbxPassword" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="requiredFieldValidator4" runat="server" ErrorMessage="* Required Field" ControlToValidate="tbxPassword" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnSignIn" runat="server" Text="Sign-In" />
            </td>
        </tr>
    </table>

</asp:Content>

