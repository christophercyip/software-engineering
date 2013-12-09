<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskPage.aspx.cs" Inherits="ConMan.TaskPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Task Page</h2>
    <hr />
    <br />
        Click the following links to perform actions:
    <ul>
        <li><asp:LinkButton ID="lnkCreate" runat="server" onclick="lnkCreate_Click">Create a Task</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkViewTask" runat="server" onclick="lnkViewTask_Click">View a Task</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkUpdateTask" runat="server" onclick="lnkUpdateTask_Click">Update a Task</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkAddRemoveMembers" runat="server" onclick="lnkAddRemoveMembers_Click">Add/Remove A User to/from a Task</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkAddNote" runat="server" onclick="lnkAddNote_Click">Add Note to a Task</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkDeleteTask" runat="server" onclick="lnkDeleteTask_Click">Delete a Task</asp:LinkButton></li>
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
                        <asp:Label ID="lblCreateTaskTeamName" runat="server" Text="Select Team: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCreateTaskTeam" runat="server" 
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
        <!-- View a Task View -->
        <asp:View ID="ViewTaskView" runat="server">
            <h3>View a Task</h3>
            <br />
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
        <!-- Add/Remove Member View -->
        <asp:View ID="MemberView" runat="server">
            <h3>Add/Remove A User to/from a Task</h3>
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblARMemTeam" runat="server" Text="Select Team: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlARMemTeam" runat="server" 
                            DataSourceID="ViewTeamsDataSource" DataTextField="team_name" 
                            DataValueField="team_id" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblARMemTask" runat="server" Text="Select Task: "></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlARMemTask" runat="server" 
                            DataSourceID="ViewTeamTasksDataSource" DataTextField="task_name" 
                            DataValueField="task_id" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="ViewTeamTasksDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:SqlDatabaseConnString %>" 
                            
                            SelectCommand="SELECT Task.task_id, Task.team_id, Task.due_date, Task.task_description, Task.task_name, Team.team_id AS TeamTeamID, Team.team_name, Team.team_owner FROM Task INNER JOIN Team ON Task.team_id = Team.team_id WHERE (Team.team_id = @team_id)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlARMemTeam" DefaultValue="0" Name="team_id" 
                                    PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblARMemEmail" runat="server" Text="Assign user to this task: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxARMemEmail" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnARMemAdd" runat="server" onclick="btnARMemAddUser_Click" 
                            Text="Add User" ValidationGroup="ARMEMGroup" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Required Field" ValidationGroup="ARMEMGroup"
                                    ControlToValidate="tbxARMemEmail"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblARMemDropDown" runat="server" Text="Select user to remove from task: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlARMemTaskUsers" runat="server" 
                            DataSourceID="ListTaskUserDataSource" DataTextField="email" 
                            DataValueField="user_id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="ListTaskUserDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:SqlDatabaseConnString %>" 
                            SelectCommand="SELECT UserTasks.user_id, UserTasks.task_id, UserInfo.id, UserInfo.first_name, UserInfo.last_name, UserInfo.email, UserInfo.password FROM UserTasks INNER JOIN UserInfo ON UserTasks.user_id = UserInfo.id WHERE (UserTasks.task_id = @task_id)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlARMemTask" DefaultValue="0" Name="task_id" 
                                    PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td>
                        <asp:Button ID="btnARMemDelete" runat="server" 
                            onclick="btnARMemRemoveUser_Click" Text="Remove User" />
                    </td>
                    <td></td>
                </tr>                
            </table>
            <asp:Label ID="lblARMemResults" runat="server" Text=""></asp:Label>
        </asp:View>
        <!-- Add Note View -->
        <asp:View ID="NoteView" runat="server">
            <h3>Add Note to a Task</h3>
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
        <asp:View ID="DeleteTaskView" runat="server">
            <h3>Delete a Task</h3>
        </asp:View>
    </asp:MultiView>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
