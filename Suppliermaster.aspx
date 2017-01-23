<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Suppliermaster.aspx.cs" Inherits="Suppliermaster" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
  <script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
  <link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />
<script type="text/javascript">
//    
    function alpha(e) {
        var k = e.charCode ? e.charCode : e.keyCode;
        return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39);   //k=9(keycode for tab)
    }

    function HideLabel() {
        var seconds = 3;
        setTimeout(function () {
            document.getElementById("<%=lblsuccess.ClientID %>").style.display = "none";
           }, seconds * 1000);
    };

    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    function toUpper(txt) {
        document.getElementById(txt).value = document.getElementById(txt).value.toUpperCase();
        return true;
    }

    function toLower(txt) {
        document.getElementById(txt).value = document.getElementById(txt).value.toLowerCase(); 
    } 
    $(document).on('keypress', '#txtcontactperson', function (event) {
        var regex = new RegExp("^[a-zA-Z ]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });
</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset class="BigFieldSet">
    <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Supplier Master
    </h1>
    </legend>
      <table id="Table2" cellpadding="10" runat="server" style="border: solid 15px Green; background-color: SkyBlue;"  cellspacing="10"  width="50%" align="center">
        <tr>
            <td align="center">
                <span style="color: Red; font-weight: bold; font-size: 18pt;"></span>&nbsp;
                <asp:Label ID="lblmod" runat="server" Text="lblmodsuc" Font-Bold="true" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
            </tr>
          <tr>
              <td align="right">
                <asp:Button ID="btn" runat="server" Width="95px" Text ="No" CssClass="Buttons embossed-link"  OnClick="btn_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
              <%-- <asp:Label ID="lblmodify" runat="server" Text="Modify" Font-Bold="true" Font-Size="Large" ForeColor="Black"></asp:Label>&nbsp;&nbsp; <asp:CheckBox ID="chkmodify" runat="server" OnCheckedChanged="chkmodify_CheckedChanged"  />--%>
                <asp:Button ID="btnmodify" runat="server" Width="95px" Text ="Modify" CssClass="Buttons embossed-link" OnClick="btnmodify_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btndelete" runat="server" Width="95px" Text ="Delete" />
               
            </td>
        </tr>
    </table>
    <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel" ></asp:Label>
    </h3>
 
              <table align="center" width="520px" cellspacing="5">
                      <tr>
                    <td align="right">
                       <asp:Label ID="lblgrpnm" runat="server" Text="Supplier Name:" CssClass="Label"></asp:Label>
                    </td>
                
                          <td>
                               <asp:TextBox ID="txtsupplier" width="253px" runat="server"  AutoPostBack="true" TabIndex="1"
                                  onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)" ontextchanged="txtsupplier_TextChanged" CssClass="TextBox"/>
                                  <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                            <%--  /<%--/ <asp:HiddenField ID="hfCustomerId" runat="server" />  -  CompletionListItemCssClass="OtherCompletionItemCssClass"-%>--%>
                               <asp:AutoCompleteExtender ID="txtsupplier_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Buyername" ServicePath="" TargetControlID="txtsupplier" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
                    </asp:AutoCompleteExtender>
                          </td>
                           <td align="right">
                              <asp:Label ID="lblcode" runat="server" Text="GN_code"></asp:Label>
                          </td>
                      </tr>

                       <tr>
                    <td align="right">
                       <asp:Label ID="Label1" runat="server" Text="Addres1:" CssClass="Label"></asp:Label>
                    </td>
                
                          <td>
                              <asp:TextBox ID="txtaddress1" width="253px" runat="server" onkeyup="return toUpper(this.id)"  AutoPostBack="true" onkeydown="return(event.keyCode != 13)" TabIndex="2" CssClass="TextBox"  ontextchanged="txtaddress1_TextChanged"></asp:TextBox>
                              <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                          </td>
                      </tr>

                       <tr>
                    <td align="right">
                       <asp:Label ID="Label2" runat="server" Text="Addres2:" CssClass="Label"></asp:Label>
                    </td>
 
                          <td>
                              <asp:TextBox ID="txtaddress2" width="253px" runat="server" onkeyup="return toUpper(this.id)" onkeydown="return(event.keyCode != 13)"  AutoPostBack="true" TabIndex="3" CssClass="TextBox"  ontextchanged="txtaddress2_TextChanged"></asp:TextBox>
                              <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                          </td>
                      </tr>

                       <tr>
                    <td align="right">
                       <asp:Label ID="Label3" runat="server" Text="City:"  CssClass="Label"></asp:Label>
                    </td>
                
                          <td>
                              <asp:TextBox ID="txtaddress3" width="253px" runat="server" onkeyup="return toUpper(this.id)" onkeydown="return(event.keyCode != 13)" AutoPostBack="true" onkeypress="return alpha(event)" TabIndex="4" CssClass="TextBox" ontextchanged="txtaddress3_TextChanged"></asp:TextBox>
                              <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                          </td>
                      </tr>

                       <tr>
                    <td align="right">
                       <asp:Label ID="Label4" runat="server" Text="Phone:" CssClass="Label" ></asp:Label>
                    </td>
                
                     <td>
                        <asp:TextBox ID="txtphone" width="253px" runat="server" MaxLength="15" onkeyup="return toUpper(this.id)" onkeypress="return isNumberKey(event)" AutoPostBack="true" onkeydown="return(event.keyCode != 13)" TabIndex="5" CssClass="TextBox"  ontextchanged="txtphone_TextChanged"></asp:TextBox>
                        <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                        
                           
                          </td>
                      </tr>

                       <tr>
                    <td align="right">
                       <asp:Label ID="Label5" runat="server" Text="Email:" CssClass="Label"></asp:Label>
                    </td>
                
                          <td>
                              <asp:TextBox ID="txtemail" width="253px" runat="server" AutoPostBack="True" CssClass="TextBox"
                                  ontextchanged="txtemail_TextChanged" onkeydown="return(event.keyCode != 13)" onkeyup="return toLower(this.id)" TabIndex="6"></asp:TextBox>
                                  <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                          </td>
                      </tr>

                      <tr>
                    <td align="right">
                       <asp:Label ID="Label7" runat="server" Text="Mobile No:" CssClass="Label"></asp:Label>
                    </td>
                
                          <td>
                              <asp:TextBox ID="txtmobilenor" width="253px" runat="server" MaxLength="10" AutoPostBack="True" onkeypress="return isNumberKey(event)" onkeyup="return toUpper(this.id)"  onkeydown="return(event.keyCode != 13)" TabIndex="7" CssClass="TextBox" OnTextChanged="txtmobilenor_TextChanged"></asp:TextBox>
                              <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                              
                          </td>
                      </tr>

                      



                       <tr>
                    <td align="right">
                       <asp:Label ID="Label6" runat="server" Text="Contact Person:" CssClass="Label"></asp:Label>
                    </td>
                
                          <td>
                              <asp:TextBox ID="txtcontactperson" width="253px" runat="server" AutoPostBack="True" onkeyup="return toUpper(this.id)" onkeydown="return(event.keyCode != 13)" TabIndex="8" CssClass="TextBox" onkeypress="return alpha(event)"  OnTextChanged="txtcontactperson_TextChanged"></asp:TextBox>
                              <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                          </td>
                      </tr>

                    

                       <tr>
                    <td align="right">
                       <asp:Label ID="Label9" runat="server" Text="Contact Person Phone:"  CssClass="Label"></asp:Label>
                    </td>
                
                          <td>
                              <asp:TextBox ID="txtcontactpersphone" width="253px" runat="server" 
                                  AutoPostBack="True" onkeydown="return(event.keyCode != 13)" MaxLength="10" 
                                  onkeyup="return toUpper(this.id)" onkeypress="return isNumberKey(event)" 
                                  TabIndex="9" CssClass="TextBox" ontextchanged="txtcontactpersphone_TextChanged"></asp:TextBox>
                              <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                              
                              
                          </td>

                      </tr>
              <tr>
                    <td align="right">
                       <asp:Label ID="LblPAN" runat="server" Text="PAN No:" CssClass="Label"></asp:Label>
                    </td>
                    <td>
                              <asp:TextBox ID="txtPAN" width="253px" runat="server" AutoPostBack="True" 
                                  TabIndex="10" CssClass="TextBox" ontextchanged="txtPAN_TextChanged"></asp:TextBox>
                              
                    </td>
              </tr>
              <tr>
                    <td align="right">
                       <asp:Label ID="Label8" runat="server" Text="TIN No:" CssClass="Label"></asp:Label>
                    </td>
                    <td>
                              <asp:TextBox ID="txtTin" width="253px" runat="server" AutoPostBack="True" 
                                  TabIndex="11" CssClass="TextBox" ontextchanged="txtTin_TextChanged"></asp:TextBox>
                              
                    </td>
              </tr>
              <tr>
                    <td align="right">
                       <asp:Label ID="Label10" runat="server" Text="Adhar No:" CssClass="Label"></asp:Label>
                    </td>
                    <td>
                              <asp:TextBox ID="txtAdhar" width="253px" runat="server" AutoPostBack="True" 
                                  TabIndex="12" CssClass="TextBox" ontextchanged="txtAdhar_TextChanged"></asp:TextBox>
                              
                    </td>
              </tr>
              <tr>
                    <td align="right">
                       <asp:Label ID="Label11" runat="server" Text="GST No:" CssClass="Label"></asp:Label>
                    </td>
                    <td>
                              <asp:TextBox ID="TxtGST" width="253px" runat="server" AutoPostBack="True" 
                                  TabIndex="13" CssClass="TextBox" ontextchanged="TxtGST_TextChanged"></asp:TextBox>
                              
                    </td>
              </tr>
              <tr>
                    <td align="right">
                       <asp:Label ID="Label12" runat="server" Text="Credit Limit:" CssClass="Label"></asp:Label>
                    </td>
                    <td>
                              <asp:TextBox ID="TxtCreLim" width="253px" runat="server" AutoPostBack="True" 
                                  TabIndex="14" CssClass="TextBox" ontextchanged="TxtCreLim_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox>
                              
                    </td>
              </tr>
              </table>
                          
          <div style="width: 980px; margin: 0 auto; padding: 0" align="center">
               <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
           
            <asp:GridView ID="Gridsupp" runat="server" CellPadding="4" BackColor="Yellow"
             AllowPaging="true"  Font-Bold="true" ForeColor="Black" Font-Names="Times New Roman" Font-Size="Small"
                      GridLines="Both" OnPageIndexChanging="Gridsupp_PageIndexChanging" PageSize="5">
         <PagerStyle BackColor="Yellow" ForeColor="Black" HorizontalAlign="Center" Font-Size="12px"/>
         </asp:GridView>
                 </asp:Panel>
             
         
               </div>
              <div align="center" class="SubmitButtons" style="position: relative; left: 350px; top: 0px;">
              
                <asp:Button ID="Button1" runat="server" Text="Save" Width="95px" OnClick="Button1_Click1" cssclass="Buttons embossed-link" TabIndex="15"/>
               <asp:Button ID="Button2" runat="server" style="margin-bottom: 0px" Text="Exit" TabIndex="16" 
                      Width="90px" cssclass="Buttons embossed-link" onclick="Button2_Click"/>
            </div>
   
        
 </fieldset> 
</asp:Content>

