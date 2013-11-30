<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskPage.aspx.cs" Inherits="ConMan.TaskPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Task Page</h2>
    <hr />
    <br />
        Click the following links to perform actions:
    <ul>
        <li><asp:LinkButton ID="lnkCreate" runat="server" onclick="lnkCreate_Click">Create Task</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkAddRemoveMembers" runat="server" onclick="lnkAddRemoveMembers_Click">Add/Remove Users</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkAddNote" runat="server" onclick="lnkAddNote_Click">Add Note</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkUpdateTask" runat="server" onclick="lnkUpdateTask_Click">Update Task</asp:LinkButton></li>
    </ul>
    <hr />
    <asp:MultiView ID="TaskMultiView" runat="server">
        <!-- Create Task View -->
        <asp:View ID="CreateView" runat="server">
            <h3>Create a Task</h3>
            <br />
           <table>
                <tr>
                    <td>
                        <asp:Label ID="lblCreateTaskName" runat="server" Text="Task Name: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxCreateTaskName" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Required Field" ValidationGroup="CreateTaskGroup"
                                    ControlToValidate="tbxCreateTaskName"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCreateTaskDueDate" runat="server" Text="Due Date: "></asp:Label></td>
                    <td>
                        <asp:Calendar ID="cdlCreateCalendar" runat="server"></asp:Calendar>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCreateDescription" runat="server" Text="Description: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxCreateDescription" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Required Field" ValidationGroup="CreateTaskGroup"
                                    ControlToValidate="tbxCreateDescription"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCreateTeamName" runat="server" Text="Select Team: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCreateTeam" runat="server" 
                            DataSourceID="ViewTeamsDataSource" DataTextField="team_name" 
                            DataValueField="team_id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="ViewTeamsDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:SqlDatabaseConnString %>" 
                            SelectCommand="SELECT * FROM [Team] ORDER BY [team_name]">
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnCreateTask" runat="server" Text="Create Task" 
                            ValidationGroup="CreateTaskGroup" onclick="btnCreateTask_Click"/></td>
                </tr>
            </table>
            <asp:Label ID="lblCreateTaskResults" runat="server" Text=""></asp:Label>
        </asp:View>
        <!-- Add/Remove Member View -->
        <asp:View ID="MemberView" runat="server">
            <h3>Add/Remove A User</h3>
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblARMemTeam" runat="server" Text="Select Team: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlARMemTeam" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblARMemTask" runat="server" Text="Select Task: "></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlARMemTask" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblARMemEmail" runat="server" Text="Enter User Email: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxARMemEmail" runat="server" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Required Field" ValidationGroup="ARMEMGroup"
                                    ControlToValidate="tbxARMemEmail"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnARMemAdd" runat="server" Text="Add User" 
                            ValidationGroup="ARMEMGroup" onclick="btnARMemAddUser_Click"/></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnARMemDelete" runat="server" Text="Remove User" 
                            ValidationGroup="ARMEMGroup" onclick="btnARMemRemoveUser_Click"/></td>
                </tr>
                
            </table>
        </asp:View>
        <!-- Add Note View -->
        <asp:View ID="NoteView" runat="server">
            <h3>Add Note to Task</h3>
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblAddNoteTeam" runat="server" Text="Select Team: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAddNoteTeam" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAddNoteTask" runat="server" Text="Select Task: "></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlAddNoteTask" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblAddNoteNote" runat="server" Text="Enter Note: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxAddNoteNote" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Required Field" ValidationGroup="AddNoteGroup"
                                    ControlToValidate="tbxAddNoteNote"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddNoteButton" runat="server" Text="Add Note" 
                            ValidationGroup="AddNoteGroup" onclick="btnAddNoteButton_Click"/></td>
                </tr>
            </table>
        </asp:View>
        <!-- Update Task View -->
        <asp:View ID="UpdateTaskView" runat="server">
            <h3>Update a Task</h3>
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblUpdateTeam" runat="server" Text="Select Team: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlUpdateTeam" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblUpdateTask" runat="server" Text="Select Task: "></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlUpdateTask" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblUpdateDate" runat="server" Text="Select New Due Date: "></asp:Label>
                    </td>
                    <td>
                        <asp:Calendar ID="cdlUpdateDate" runat="server"></asp:Calendar>
                    </td>
                    <td>
                        <asp:Button ID="btnUpdateDate" runat="server" Text="Update Due Date"  />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblUpdateDescription" runat="server" Text="Enter New Description: "></asp:Label></td>
                    <td>
                        <asp:TextBox ID="tbxUpdateDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnUpdateDescription" runat="server" Text="Update Description"  ValidationGroup="UpdateTaskGroup" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* Required Field" ValidationGroup="UpdateTaskGroup"
                                    ControlToValidate="tbxUpdateDescription"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
