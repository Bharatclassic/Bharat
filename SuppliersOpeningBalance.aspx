<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="SuppliersOpeningBalance.aspx.cs" Inherits="SuppliersOpeningBalance" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
<script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
<link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />
 <script type="text/javascript">
    function HideLabel() {
        var seconds = 3;
        setTimeout(function () {
            document.getElementById("<%=lblsuccess.ClientID %>").style.display = "none";
        }, seconds * 1000);
    };

</script>

<asp:Panel ID="Panel1" runat="server" Width="1000px" > 

 <fieldset class="BigFieldSet">
    <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Supplier Opening Balance
    </h1>
    </legend>
    
    <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>

    </h3>
     <table id="tbl" align="right">
                <tr>
                <td>
                <asp:Label ID="lblcutoffdate" Text="Cut off Date :" runat="server" CssClass="Label"></asp:Label>
               &nbsp;<asp:TextBox ID="txtcutoffdate" runat="server" AutoPostBack="true" Width="80px"></asp:TextBox>
                    &nbsp;
               
                
                </td>
                </tr>
                </table>

                <div  style="margin-left:75px;">
              
            <table id="Table1" align="center">
                      <tr>
                    <td align="center">
                <asp:Label ID="lblsupcode" Text="Supplier Code:" runat="server" CssClass="Label"></asp:Label>
                </td>
               
                <td>
                <asp:TextBox ID="txtsupcode" runat="server" CssClass="TextBox" Width="80px" AutoPostBack="true" onkeypress="return alpha1(event)" TabIndex='1'
                        ontextchanged="txtsupcode_TextChanged"></asp:TextBox>
                        <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                <asp:AutoCompleteExtender ID="txtsupcode_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Suppliercode" ServicePath="" TargetControlID="txtsupcode" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
                    </asp:AutoCompleteExtender>
                
                </td>
                             <td>
                                   
                               </td>

                                <td>
                <asp:Label ID="lblsupname" Text="Supplier Name:" runat="server" CssClass="Label">
                </asp:Label>
                
                </td>
                
                <td>
                <asp:TextBox ID="txtsupname" runat="server" CssClass="TextBox" width="200px"  AutoPostBack="true" onkeypress="return alpha(event)" onkeyup="return toUpper(this.id)" TabIndex='2'
                        ontextchanged="txtsupname_TextChanged" ></asp:TextBox>
                        <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                <asp:AutoCompleteExtender ID="txtsupname_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Suppliername" ServicePath="" TargetControlID="txtsupname" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
                    </asp:AutoCompleteExtender>
                
                </td>
                          
                           
                          </tr>
                          </table>
                          <br />

                          </div>

                          <table align="center">
                          <tr align="center" >
                          <td>
                          <asp:Label ID="lblbalance" runat="server" Text="Balance Value in Rs:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
                          </td>
                          <td>
                         <asp:TextBox ID="txtbalance" runat="server" AutoPostBack="true" TabIndex='3' ontextchanged="txtbalance_TextChanged"></asp:TextBox>
                         
                          </td>
                          </tr>
                          </table>
                          <br />

                           <div align="left" class="SubmitButtons" style="position:relative; left: 450px; top: 0px;"> 
             <asp:Button ID="btnsave" runat="server" Text="Save"  Width="95px" TabIndex='4'
                    CssClass="Buttons embossed-link" onclick="btnsave_Click"/>
              <asp:Button ID="btnExit" runat="server" Text="Exit" Width="95px" TabIndex='5'
                    CssClass="Buttons embossed-link" onclick="btnexit_Click"/>  
                </div> 
           
                          
           </fieldset>
        </asp:Panel>
        
           

 
</asp:Content>


