<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="PurchaseReturnStock.aspx.cs" Inherits="PurchaseReturnStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset class="BigFieldSet">
<h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
    </h3>
<table width="400">
<tr>
 <td>
                            <asp:Label ID="lblPname" runat="server" Text="Enter New Stock:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>

                            </td>
                            <td>
                            <asp:TextBox ID="txtstockhand" runat="server" Width="150px" AutoPostBack="true"
                                     onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)"></asp:TextBox>
                                    
                                      
                                     
                            </td>

                            <asp:Label ID="lblstockhand" runat="server" Text="" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>

</tr>

</table>

 <div align="left" class="SubmitButtons" style="position:relative; left: 50px; top: 0px;"> 
             <asp:Button ID="btnupdate" runat="server" Text="Update"  Width="95px" 
                 TabIndex="2" CssClass="Buttons embossed-link" onclick="btnupdate_Click"/>
              <asp:Button ID="btnExit" runat="server" Text="Exit" Width="95px" CssClass="Buttons embossed-link" OnClick="btnExit_Click" TabIndex="3"/>  
                </div>
                
                </fieldset> 

</asp:Content>

