<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddEditLabsaspx.aspx.cs" Inherits="AddEditLabsaspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 270px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <asp:GridView ID="gvLabs" runat="server" OnRowDeleting="OnRowDeleting" EmptyDataText="No records has been added." BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" DataKeyNames="labID" OnSelectedIndexChanged="gvLabs_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
        <Columns>
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
            <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
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
            <asp:Button ID="btnAddEditLabs" runat="server" CausesValidation="False" OnClick="btnAddEditLabs_Click" Text="Click here to add labs" />
        </div>

    <asp:Panel ID="pnlLabs" runat="server" Direction="LeftToRight" Height="16px" Visible="False">
        <table class="auto-style1">
             
            <tr>
                <td class="auto-style2">Lab Type</td>
                <td>
                    <asp:TextBox ID="txtLabType" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLabID0" runat="server" ControlToValidate="txtLabType" ErrorMessage="Please enter Lab Type"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Organization </td>
                <td>
                    <asp:DropDownList ID="ddlOrganization" runat="server">
                        <asp:ListItem Value="99">--Select--</asp:ListItem>
                        <asp:ListItem Value="101">University</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">College</td>
                <td>
                    <asp:DropDownList ID="ddlCollege" runat="server">
                        <asp:ListItem Value="99">--Select--</asp:ListItem>
                        <asp:ListItem Value="201">College of Business</asp:ListItem>
                        <asp:ListItem Value="202">Col Liberal Arts &amp; Science</asp:ListItem>
                        <asp:ListItem Value="203">Col of Arts &amp; Architecture</asp:ListItem>
                        <asp:ListItem Value="204">Academic Affairs</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Department</td>
                <td>
                    <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem Value="99">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Lab Area</td>
                <td>
                    <asp:TextBox ID="txtLabArea" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>
                    <asp:Button ID="btnLab" runat="server" Text="Add Lab" OnClick="btnLab_Click" />
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel" OnClick="btnCancel_Click" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlUpdateLabs" runat="server" Visible="False">
            <table class="auto-style1">
            <tr>
                <td class="auto-style2">Lab ID</td>
                <td>
                    <asp:TextBox ID="txtLabID" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr> 
            <tr>
                <td class="auto-style2">Lab Type</td>
                <td>
                    <asp:TextBox ID="txtLabType1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Organization </td>
                <td>
                    <asp:DropDownList ID="ddlOrganization1" runat="server">
                        <asp:ListItem Value="99">--Select--</asp:ListItem>
                        <asp:ListItem Value="101">University</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">College</td>
                <td>
                    <asp:DropDownList ID="ddlCollege1" runat="server">
                        <asp:ListItem Value="99">--Select--</asp:ListItem>
                        <asp:ListItem Value="201">College of Business</asp:ListItem>
                        <asp:ListItem Value="202">Col Liberal Arts &amp; Science</asp:ListItem>
                        <asp:ListItem Value="203">Col of Arts &amp; Architecture</asp:ListItem>
                        <asp:ListItem Value="204">Academic Affairs</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Department</td>
                <td>
                    <asp:DropDownList ID="ddlDepartment1" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem Value="99">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Lab Area</td>
                <td>
                    <asp:TextBox ID="txtLabArea1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>
                    <asp:Button ID="btnUpdateLabs" runat="server" Text="Update" OnClick="btnUpdateLabs_Click" />
                    <asp:Button ID="btnCancel1" runat="server" CausesValidation="False" Text="Cancel" OnClick="btnCancel1_Click" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        </asp:Panel>

</asp:Content>

