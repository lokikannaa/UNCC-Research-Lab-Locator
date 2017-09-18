<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditUser.aspx.cs" Inherits="EditUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    Edit User
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>--%>

            <asp:GridView ID="gvUser" runat="server" OnRowDeleting="OnRowDeleting" EmptyDataText="No records has been added." BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnSelectedIndexChanged="gvUser_SelectedIndexChanged" DataKeyNames="userID">
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                <Columns>
                    <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                    <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                </Columns>
                <FooterStyle BackColor="Tan" />
                <HeaderStyle BackColor="Tan" Font-Bold="True" />
                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                <SortedAscendingCellStyle BackColor="#FAFAE7" />
                <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                <SortedDescendingCellStyle BackColor="#E1DB9C" />
                <SortedDescendingHeaderStyle BackColor="#C2A47B" />
            </asp:GridView>
    <br />
    <br />
        <div>
            <asp:Button ID="btnAddUserLink" runat="server" OnClick="btnAddUserLink_Click" Text="Click here to Add User" CausesValidation="False" />
        </div>      
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:Panel ID="pnlAddUser" runat="server" Visible="False">
            <table style="text-align:center">
                <tr>
                    <td>User ID</td>
                    <td>
                        <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
                    </td>
                    <td>

                        <asp:RequiredFieldValidator ID="rfvUserID" runat="server" ControlToValidate="txtUserID" ErrorMessage="Please Enter UserID" ForeColor="Red"></asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td>First Name</td>
                    <td>
                        <asp:TextBox ID="txtFName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Last Name</td>
                    <td>
                        <asp:TextBox ID="txtLName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Designation</td>
                    <td>
                        <asp:DropDownList ID="ddlDesignation" runat="server">
                            <asp:ListItem Value="99">--Select--</asp:ListItem>
                            <asp:ListItem Value="0">Admin</asp:ListItem>
                            <asp:ListItem Value="1">Dean</asp:ListItem>
                            <asp:ListItem Value="3">Dept Chair</asp:ListItem>
                            <asp:ListItem Value="4">Vice Provost</asp:ListItem>
                            <asp:ListItem Value="5">Faculty</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Password</td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>DepartmentID</td>
                    <td>
                        <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="True">
                            <asp:ListItem Value="99">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnAddUser" runat="server" Text="Add User" OnClick="btnAddUser_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlDeleteEditUser" runat="server" Direction="LeftToRight" Height="16px" Visible="False">
            <table style="text-align:center">
                <tr>
                    <td>User ID</td>
                    <td>
                        <asp:TextBox ID="txtUserID0" runat="server" Width="128px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>First Name</td>
                    <td>
                        <asp:TextBox ID="txtFName0" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Last Name</td>
                    <td>
                        <asp:TextBox ID="txtLName0" runat="server"></asp:TextBox>
                    </td>
                </tr>             
                <tr>
                    <td>Password</td>
                    <td>
                        <asp:TextBox ID="txtPassword0" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" />
                        <asp:Button ID="btnCancel0" runat="server" Text="Cancel" CausesValidation="False" OnClick="btnCancel0_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
</asp:Content>

