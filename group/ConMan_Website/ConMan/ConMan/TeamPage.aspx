<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeamPage.aspx.cs" Inherits="ConMan.TeamPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Team Page</h2>
    <hr />
    <br />
        Click the following links to perform actions:
    <ul>
        <li><asp:LinkButton ID="lnkCreate" runat="server" onclick="lnkCreate_Click">Create Team</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkView" runat="server" onclick="lnkView_Click">View Team</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkUpdate" runat="server" onclick="lnkUpdate_Click">Update Team</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkDelete" runat="server" onclick="lnkDelete_Click">Delete Team</asp:LinkButton></li>
    </ul>
    <hr />
    <asp:MultiView ID="TeamMultiView" runat="server">
        <!-- Create View -->
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
                            Width="200px" BackColor="#CCCCCC"></asp:TextBox>
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
        <!-- Read View -->
        <asp:View ID="ReadView" runat="server">
            <h3>View a Team</h3>
            <p>
                TODO: Add radio buttons that will allow the user to display either all Teams or 
                all teams that they are members of.
            </p>
            <br />
            <asp:DropDownList ID="ddlTeamsView" runat="server" 
                DataSourceID="ViewTeamsDataSource" DataTextField="team_name" 
                DataValueField="team_id"></asp:DropDownList>
            <asp:SqlDataSource ID="ViewTeamsDataSource" runat="server" 
                ConnectionString="<%$ ConnectionStrings:SqlDatabaseConnString %>" 
                SelectCommand="SELECT [team_id], [team_name] FROM [Team]">
            </asp:SqlDataSource>
            <asp:Button ID="btnViewTeam" runat="server" Text="View Team Information" 
                onclick="btnViewTeam_Click" />
            <br />
            <br />
            <table>
                <tr>
                    <td>Team Name: </td>
                    <td><asp:TextBox ID="tbxViewTeamName" runat="server" BackColor="#CCCCCC" 
                            MaxLength="50" ReadOnly="True" Width="200px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Team Owner: </td>
                    <td><asp:TextBox ID="tbxViewTeamOwner" runat="server" BackColor="#CCCCCC" 
                            ReadOnly="True" Width="200px"></asp:TextBox></td>
                </tr>
            </table>
            <asp:Label ID="lblViewTeamResults" runat="server" Text=""></asp:Label>
        </asp:View>
        <!-- Update View -->
        <asp:View ID="UpdateView" runat="server">
            <h3>Update a Team</h3>
            <p>
                First, select the team you wish to update in the first drop down list. To 
                add a new member to the team, enter the member's email address and click the 
                adjacent button to add the member. To remove a member from the team, select 
                the member's email address from the drop-down list and click the adjacent button. 
                NOTE: Only a team's admin can update a team.
            </p>
            
            <br />
            <table>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlTeamsUpdate" runat="server" 
                            DataSourceID="ViewTeamsDataSource" DataTextField="team_name" 
                            DataValueField="team_id" AutoPostBack="True"></asp:DropDownList>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Add Team Member: </td>
                    <td><asp:TextBox ID="tbxUpdateNewMember" runat="server" MaxLength="50"></asp:TextBox></td>
                    <td><asp:Button ID="btnUpdateAddMember" runat="server" Text="Add Member" 
                            ValidationGroup="UpdateNewMemberGroup" onclick="btnUpdateAddMember_Click"/></td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbxUpdateNewMember" ErrorMessage="* Required Field" ValidationGroup="UpdateNewMemberGroup"/>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbxUpdateNewMember" ErrorMessage="* Field must be a valid email address." ValidationExpression="^\S+@\S+\.\S+$" ValidationGroup="UpdateNewMemberGroup"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>Remove Team Member: </td>
                    <td>
                        <asp:DropDownList ID="ddlUpdateRemoveMember" runat="server" 
                            DataSourceID="ViewTeamMembersDataSource" DataTextField="email" 
                            DataValueField="id"></asp:DropDownList>
                        <asp:SqlDataSource ID="ViewTeamMembersDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:SqlDatabaseConnString %>" 
                            
                            SelectCommand="SELECT TeamMember.team_mem_id, TeamMember.user_mem_id, TeamMember.mem_admin, UserInfo.id, UserInfo.first_name, UserInfo.last_name, UserInfo.email, UserInfo.password FROM TeamMember INNER JOIN UserInfo ON TeamMember.user_mem_id = UserInfo.id WHERE (TeamMember.team_mem_id = @team_id)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlTeamsUpdate" DefaultValue="0" 
                                    Name="team_id" Type="Int64" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td><asp:Button ID="btnUpdateRemoveMember" runat="server" Text="Remove Member" 
                            onclick="btnUpdateRemoveMember_Click" /></td>
                    <td></td>
                </tr>
            </table>
            <asp:Label ID="lblUpdateResults" runat="server" Text=""></asp:Label>
        </asp:View>
        <!-- Delete View -->
        <asp:View ID="DeleteView" runat="server">
            <h3>Delete a Team</h3>
            <br />
            <asp:DropDownList ID="ddlTeamsDelete" runat="server" 
                DataSourceID="ViewTeamsDataSource" DataTextField="team_name" 
                DataValueField="team_id">
            </asp:DropDownList>
            <asp:Button ID="btnDeleteTeam" runat="server" Text="Delete Team" 
                onclick="btnDeleteTeam_Click" />
            <br />
            <asp:Label ID="lblDeleteTeamResults" runat="server" Text=""></asp:Label>
        </asp:View>
    </asp:MultiView>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
