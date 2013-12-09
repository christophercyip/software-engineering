<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="ConMan.SignIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Sign-In</h2>
    <hr />
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Email: "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbxEmailSignIn" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="requiredFieldValidator6" runat="server" ErrorMessage="* Required Field" ControlToValidate="tbxEmailSignIn" ValidationGroup="SignInGroup"/>
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Password: "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbxPasswordSignIn" runat="server" TextMode="Password" 
                    MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="requiredFieldValidator7" runat="server" ErrorMessage="* Required Field" ControlToValidate="tbxPasswordSignIn" ValidationGroup="SignInGroup"/>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnSignIn" runat="server" Text="Sign-In" 
                    onclick="btnSignIn_Click" ValidationGroup="SignInGroup"/>
            </td>
        </tr>
        <asp:Label ID="lblResultsSignIn" runat="server" Text=""></asp:Label>
    </table>
    <h2>
        Register
    </h2>
    <hr />
    <table>
        <tr>
            <td>
                <asp:Label ID="lblFirstName" runat="server" Text="First Name: "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbxFirstNameRegister" runat="server" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="requiredFieldValidator1" runat="server" ErrorMessage="* Required Field" ControlToValidate="tbxFirstNameRegister" ValidationGroup="RegistrationGroup"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblLastName" runat="server" Text="Last Name: "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbxLastNameRegister" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="requiredFieldValidator2" runat="server" ErrorMessage="* Required Field" ControlToValidate="tbxLastNameRegister" ValidationGroup="RegistrationGroup"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEmail" runat="server" Text="Email: "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbxEmailRegister" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="requiredFieldValidator3" runat="server" ErrorMessage="* Required Field" ControlToValidate="tbxEmailRegister" ValidationGroup="RegistrationGroup"/>
                <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidator1" runat="server" ErrorMessage="* Please enter a valid email address" 
                    ControlToValidate="tbxEmailRegister" ValidationExpression="^\S+@\S+\.\S+$" ValidationGroup="RegistrationGroup"/>
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblPassword" runat="server" Text="Password: "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbxPasswordRegister" runat="server" TextMode="Password" 
                    MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="requiredFieldValidator4" runat="server" ErrorMessage="* Required Field" ControlToValidate="tbxPasswordRegister" ValidationGroup="RegistrationGroup"/>
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password: "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbxConfirmPasswordRegister" runat="server" TextMode="Password" 
                    MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="requiredFieldValidator5" runat="server" ErrorMessage="* Required Field" ControlToValidate="tbxConfirmPasswordRegister" ValidationGroup="RegistrationGroup"/>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        
            <td>
                <asp:Button ID="btnRegister" runat="server" Text="Register" 
                        onclick="btnSubmit_Click" ValidationGroup="RegistrationGroup"/>
            </td>
        </tr>
        <asp:Label ID="lblResultsRegister" runat="server" Text=""></asp:Label>

    </table>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
