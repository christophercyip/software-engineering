<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Registration.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Registration Page
    </h2>
<hr />
<table>
    <tr>
        <td>
            <asp:Label ID="lblFirstName" runat="server" Text="First Name: "></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tbxFirstName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="requiredFieldValidator1" runat="server" ErrorMessage="* Required Field" ControlToValidate="tbxFirstName"/>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblLastName" runat="server" Text="Last Name: "></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tbxLastName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="requiredFieldValidator2" runat="server" ErrorMessage="* Required Field" ControlToValidate="tbxLastName"/>
        </td>
    </tr>
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
            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password: "></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tbxConfirmPassword" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="requiredFieldValidator5" runat="server" ErrorMessage="* Required Field" ControlToValidate="tbxConfirmPassword" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Register" 
                    onclick="btnSubmit_Click" />
        </td>
    </tr>
    <asp:Label ID="lblResults" runat="server" Text=""></asp:Label>

</table>

    
    
</asp:Content>
