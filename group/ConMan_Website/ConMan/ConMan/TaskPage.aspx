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
                        <asp:TextBox ID="tbxCreateTaskName" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Required Field" ValidationGroup="CreateTaskGroup"
                                    ControlToValidate="tbxCreateTaskName"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCreateTaskDueDate" runat="server" Text="Due Date: "></asp:Label></td>
                    <td>
                        <asp:Calendar ID="cdlCreateCalendar" runat="server" Width="300px"></asp:Calendar>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCreateDescription" runat="server" Text="Description: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxCreateDescription" runat="server" Width="300px" 
                            TextMode="MultiLine"></asp:TextBox>
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
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblViewTeam" runat="server" Text="Select Team: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlViewTeam" runat="server" 
                            DataSourceID="ViewTeamsDataSource" DataTextField="team_name" 
                            DataValueField="team_id" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblViewTask" runat="server" Text="Select Task: "></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlViewTask" runat="server" 
                            DataSourceID="ViewTeamTasksDataSource" DataTextField="task_name" 
                            DataValueField="task_id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="ViewTeamTasksDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:SqlDatabaseConnString %>" 
                            SelectCommand="SELECT Task.task_id, Task.team_id, Task.due_date, Task.task_description, Task.task_name, Team.team_id AS TeamTeamID, Team.team_name, Team.team_owner FROM Task INNER JOIN Team ON Task.team_id = Team.team_id WHERE (Team.team_id = @team_id)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlViewTeam" DefaultValue="0" Name="team_id" 
                                    PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="btnViewTask" runat="server" Text="View Task" 
                            onclick="btnViewTask_Click" /></td>
                </tr>
            </table>
            <asp:Label ID="lblViewTaskResults" runat="server" Text=""></asp:Label>
            <hr />
            <table>
                <tr>
                    <td> Task Name:</td>
                    <td><asp:TextBox ID="tbxViewTaskName" runat="server" BackColor="#CCCCCC" 
                            ReadOnly="True" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td> Task Due Date:</td>
                    <td><asp:TextBox ID="tbxViewTaskDueDate" runat="server" BackColor="#CCCCCC" 
                            ReadOnly="True" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td> Task Description:</td>
                    <td><asp:TextBox ID="tbxViewTaskDescription" runat="server" BackColor="#CCCCCC" 
                            Height="100px" ReadOnly="True" TextMode="MultiLine" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td> Users Assigned to Task:</td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:DataList ID="ViewTaskUsersDataList" runat="server" CellPadding="4" 
                            DataKeyField="id" DataSourceID="ViewTaskUsersDataSource" 
                            ForeColor="#333333" Width="200px" Visible="False">
                            <AlternatingItemStyle BackColor="White" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <ItemStyle BackColor="#E3EAEB" />
                            <ItemTemplate>
                                Name:
                                <asp:Label ID="first_nameLabel" runat="server" 
                                    Text='<%# Eval("first_name") + " " + Eval("last_name") %>' />
                                <br />
                                Email:
                                <asp:Label ID="last_nameLabel" runat="server" Text='<%# Eval("email") %>' />
                                <br />
                                <br />
                            </ItemTemplate>
                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        </asp:DataList></td>
                </tr>
                       
            </table>
           <asp:SqlDataSource ID="ViewTaskUsersDataSource" runat="server" 
                ConnectionString="<%$ ConnectionStrings:SqlDatabaseConnString %>" 
                SelectCommand="SELECT UserInfo.id, UserInfo.first_name, UserInfo.last_name, UserInfo.email, UserTasks.task_id FROM UserInfo INNER JOIN UserTasks ON UserInfo.id = UserTasks.user_id WHERE (UserTasks.task_id = @task_id)">
               <SelectParameters>
                   <asp:ControlParameter ControlID="ddlViewTask" DefaultValue="0" Name="task_id" 
                       PropertyName="SelectedValue" />
               </SelectParameters>
            </asp:SqlDataSource>
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
                        <asp:DropDownList ID="ddlUpdateTeam" runat="server" AutoPostBack="True" 
                            DataSourceID="UpdateTeamsDataSource" DataTextField="team_name" 
                            DataValueField="team_id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="UpdateTeamsDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:SqlDatabaseConnString %>" 
                            SelectCommand="SELECT [team_id], [team_name] FROM [Team]"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblUpdateTask" runat="server" Text="Select Task: "></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlUpdateTask" runat="server" 
                            DataSourceID="UpdateTasksDataSource" DataTextField="task_name" 
                            DataValueField="task_id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="UpdateTasksDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:SqlDatabaseConnString %>" 
                            SelectCommand="SELECT Task.task_id, Task.task_name FROM Task INNER JOIN Team ON Task.team_id = Team.team_id WHERE (Task.team_id = @team_id)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlUpdateTeam" DefaultValue="0" Name="team_id" 
                                    PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnUpdateLoadTaskInfo" runat="server" 
                            Text="Refresh/Load Task Info" onclick="btnUpdateLoadTaskInfo_Click" /></td>
                </tr>
                <tr>
                    <td>Task Due Date: </td>
                    <td>
                        <asp:TextBox ID="tbxUpdateOrigDueDate" runat="server" BackColor="#CCCCCC" 
                            ReadOnly="True" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Task Description: </td>
                    <td>
                        <asp:TextBox ID="tbxUpdateOrigDescription" runat="server" BackColor="#CCCCCC" 
                            ReadOnly="True" TextMode="MultiLine" Width="300px"></asp:TextBox></td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblUpdateDate" runat="server" Text="Select New Due Date: "></asp:Label>
                    </td>
                    <td>
                        <asp:Calendar ID="cdlUpdateDate" runat="server" Width="300px"></asp:Calendar>
                    </td>
                    <td>
                        <asp:Button ID="btnUpdateDate" runat="server" Text="Update Due Date" 
                            onclick="btnUpdateDate_Click"  />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblUpdateDescription" runat="server" Text="Enter New Description: "></asp:Label></td>
                    <td>
                        <asp:TextBox ID="tbxUpdateDescription" runat="server" TextMode="MultiLine" 
                            Width="300px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnUpdateDescription" runat="server" Text="Update Description"  
                            ValidationGroup="UpdateTaskGroup" onclick="btnUpdateDescription_Click" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* Required Field" ValidationGroup="UpdateTaskGroup"
                                    ControlToValidate="tbxUpdateDescription"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblUpdateTaskResults" runat="server" Text=""></asp:Label>
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
                            DataSourceID="ARMemTeamTasksDataSource" DataTextField="task_name" 
                            DataValueField="task_id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="ARMemTeamTasksDataSource" runat="server" 
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
                        <asp:Label ID="lblARMemEmail" runat="server" Text="Assign/unassign user to/from this task: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxARMemEmail" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnARMemAdd" runat="server" onclick="btnARMemAddUser_Click" 
                            Text="Add User" ValidationGroup="ARMEMGroup" />
                        <asp:Button ID="btnARMemDelete" runat="server" 
                            onclick="btnARMemRemoveUser_Click" Text="Remove User" ValidationGroup="ARMEMGroup"/>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Required Field" ValidationGroup="ARMEMGroup"
                                    ControlToValidate="tbxARMemEmail"></asp:RequiredFieldValidator>
                        
                    </td>
                    <td><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbxARMemEmail" 
                                    ErrorMessage="* Field must be a valid email address." ValidationExpression="^\S+@\S+\.\S+$" 
                                    ValidationGroup="ARMEMGroup"></asp:RegularExpressionValidator></td>
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
                        <asp:DropDownList ID="ddlAddNoteTeam" runat="server" AutoPostBack="True" 
                            DataSourceID="ViewTeamsDataSource" DataTextField="team_name" 
                            DataValueField="team_id">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAddNoteTask" runat="server" Text="Select Task: "></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlAddNoteTask" runat="server"  DataTextField="task_name" 
                            DataValueField="task_id" DataSourceID="AddNoteTasksDataSource">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="AddNoteTasksDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:SqlDatabaseConnString %>" 
                            SelectCommand="SELECT Task.task_id, Task.team_id, Task.due_date, Task.task_description, Task.task_name, Team.team_id AS TeamTeamID, Team.team_name, Team.team_owner FROM Task INNER JOIN Team ON Task.team_id = Team.team_id WHERE (Team.team_id = @team_id)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlAddNoteTeam" DefaultValue="0" 
                                    Name="team_id" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnViewNotesRefresh" runat="server" 
                            onclick="btnViewNotesRefresh_Click" Text="Refresh/Load Notes for this Task" />
                    </td>
                    
                </tr>
                <tr>
                    <td>Task Notes: </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:DataList ID="NotesDataList" runat="server" DataKeyField="id" 
                        DataSourceID="ListTaskNotesDataSource" CellPadding="4" ForeColor="#333333" 
                        Visible="False">
                        <AlternatingItemStyle BackColor="White" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <ItemStyle BackColor="#E3EAEB" />
                        <ItemTemplate>
                            Name:
                            <asp:Label ID="lblNoteFirstAndLastName" runat="server" 
                                Text='<%# Eval("first_name") + " " + Eval("last_name")%>' />
                            <br />
                            Email:
                            <asp:Label ID="lblNoteEmail" runat="server" Text='<%# Eval("email") %>' />
                            <br />
                            Date:
                            <asp:Label ID="note_dateLabel" runat="server" Text='<%# Eval("note_date") %>' />
                            <br />
                            Note:
                            <asp:Label ID="lblNote" runat="server" Text='<%# Eval("note") %>' />
                            <br />
                            <br />
                        </ItemTemplate>
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataList>
                    <asp:SqlDataSource ID="ListTaskNotesDataSource" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SqlDatabaseConnString %>" 
                        
                        SelectCommand="SELECT TaskNotes.user_id AS Expr3, TaskNotes.note_date AS Expr4, TaskNotes.note AS Expr5, TaskNotes.task_id AS Expr6, UserInfo.email AS Expr1, UserInfo.last_name AS Expr2, UserInfo.id, UserInfo.first_name, UserInfo.last_name, UserInfo.email, UserInfo.password, TaskNotes.task_id, TaskNotes.user_id, TaskNotes.note_date, TaskNotes.note FROM TaskNotes INNER JOIN UserInfo ON TaskNotes.user_id = UserInfo.id WHERE (TaskNotes.task_id = @task_id)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlAddNoteTask" DefaultValue="0" 
                                Name="task_id" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource></td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblAddNoteNote" runat="server" Text="Enter Note: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxAddNoteNote" runat="server" Width="300px" 
                            TextMode="MultiLine"></asp:TextBox>
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
            <asp:Label ID="lblAddNoteResults" runat="server" Text=""></asp:Label>
        </asp:View>
        <asp:View ID="DeleteTaskView" runat="server">
            <h3>Delete a Task</h3>
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Select Team: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDeleteTeam" runat="server" AutoPostBack="True" 
                            DataSourceID="ViewTeamsDataSource" DataTextField="team_name" 
                            DataValueField="team_id">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Select Task: "></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlDeleteTask" runat="server"  DataTextField="task_name" 
                            DataValueField="task_id" DataSourceID="DeleteListAllTasksDataSource">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="DeleteListAllTasksDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:SqlDatabaseConnString %>" 
                            
                            SelectCommand="SELECT Task.task_id, Task.team_id, Task.due_date, Task.task_description, Task.task_name, Team.team_id AS TeamTeamID, Team.team_name, Team.team_owner FROM Task INNER JOIN Team ON Task.team_id = Team.team_id WHERE (Team.team_id = @team_id)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlDeleteTeam" DefaultValue="0" 
                                    Name="team_id" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnDeleteTask" runat="server" Text="Delete Task" 
                            onclick="btnDeleteTask_Click" /></td>
                </tr>
            </table>
            <asp:Label ID="lblDeleteTaskResults" runat="server" Text=""></asp:Label>
        </asp:View>
    </asp:MultiView>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
