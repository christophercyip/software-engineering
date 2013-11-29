<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeamPage.aspx.cs" Inherits="ConMan.TeamPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Team Page</h2>
    <hr />
    <br />
        Click the following links to perform actions:
    <ul>
        <li><asp:LinkButton ID="lnkRead" runat="server" onclick="lnkRead_Click">Create Team</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkView" runat="server" onclick="lnkView_Click">View Team</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkUpdate" runat="server" onclick="lnkUpdate_Click">Update Team</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkDelete" runat="server" onclick="lnkDelete_Click">Delete Team</asp:LinkButton></li>
    </ul>
    <hr />
    <asp:MultiView ID="TeamMultiView" runat="server">
        <asp:View ID="CreateView" runat="server">
            <h3>Create a Team</h3>
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblCreateTeamName" runat="server" Text="Team Name: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxCreateTeamName" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Required Field" ValidationGroup="CreateTeamGroup"
                                    ControlToValidate="tbxCreateTeamName"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCreateTeamOwner" runat="server" Text="Team Owner: "></asp:Label></td>
                    <td>
                        <asp:TextBox ID="tbxCreateTeamOwner" runat="server" ReadOnly="True" 
                            Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Required Field" ValidationGroup="CreateTeamGroup"
                                    ControlToValidate="tbxCreateTeamOwner"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnCreateTeam" runat="server" Text="Create Team" 
                            ValidationGroup="CreateTeamGroup" onclick="btnCreateTeam_Click"/></td>
                </tr>
            </table>
            <asp:Label ID="lblCreateTeamResults" runat="server" Text="Label"></asp:Label>
        </asp:View>
        <asp:View ID="ReadView" runat="server">
            <h3>View a Team</h3>
            <br />
            <asp:DropDownList ID="ddlTeams" runat="server" 
                DataSourceID="ViewTeamsDataSource" DataTextField="team_name" 
                DataValueField="team_id"></asp:DropDownList>
            <asp:SqlDataSource ID="ViewTeamsDataSource" runat="server" 
                ConnectionString="<%$ ConnectionStrings:SqlDatabaseConnString %>" 
                SelectCommand="SELECT [team_id], [team_name] FROM [Team]">
            </asp:SqlDataSource>
            <asp:Button ID="btnViewTeam" runat="server" Text="View Team Information" />
        </asp:View>
        <asp:View ID="UpdateView" runat="server">
            <h3>Update a Team</h3>
        </asp:View>
        <asp:View ID="DeleteView" runat="server">
            <h3>Delete a Team</h3>
        </asp:View>
    </asp:MultiView>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
