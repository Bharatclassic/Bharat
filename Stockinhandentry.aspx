<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Stockinhandentry.aspx.cs" Inherits="Stockinhandentry" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">
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



    function validate(e, txt) {
        var KeyID = e.keyCode;
        if (KeyID == 9) {
            if (txt.value == '') {
                txt.focus();
                return false;
            }
        }
    }

    function alpha(e) {
        var k = e.charCode ? e.charCode : e.keyCode;
        return ((k > 48 && k < 57) || (k > 96 && k < 123) || (k > 64 && k < 91) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39);   //k=9(keycode for tab) ||
    }
    function alpha1(e) {
        var k = e.charCode ? e.charCode : e.keyCode;
        return ((k >= 48 && k <= 57) || (k > 64 && k < 91) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39 || k == 46);   //k=9(keycode for tab) ||
    }
      
       </script>

    
      
    <style type="text/css">
        .style12
        {
            width: 119px;
        }
        .style13
        {
            width: 150px;
        }
        .style14
        {
            width: 109px;
        }
        .style15
        {
            width: 133px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <fieldset class="BigFieldSet" >
    <legend class="BigLegend">
    <h1 style="position: relative;">
     Stock In Hand Entry
    </h1>
    </legend>

       
    
    
    <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
    </h3>

     <asp:UpdatePanel ID="UpdatePanel3" 
                 UpdateMode="Conditional"
                 runat="server">
                 <ContentTemplate>
   
   <table align="center">
   <tr>

    <td>
                                   <div align="center">
                                                       <asp:Label ID="lblsupplier" runat="server" CssClass="Label" Font-Bold="true" 
                                                           Font-Size="Medium" Text="Supplier Name:"></asp:Label>
                                                           </div>
                                                   </td>
                                                   
                                                   <td>
                                                   <asp:UpdatePanel ID="UpdatePanel1" 
                 UpdateMode="Conditional"
                 runat="server">
                 <ContentTemplate>
                                                       <asp:DropDownList ID="ddlsupplier" runat="server" AutoPostBack="true" height="25px" 
                                                           CssClass="DropDown" onselectedindexchanged="ddlsupplier_SelectedIndexChanged" Width="150px">
                                                       </asp:DropDownList>
                                                       </ContentTemplate>
</asp:UpdatePanel>
                                                  
                                   <asp:Label ID="lblsuppliercode" runat="server" Text="" Font-Size="Medium" Font-Bold="true" CssClass="Label" Visible='false'></asp:Label>
                              </td>

   
     </tr>
   
   </table>
   

   
     
   
                      

   

  <table width="1000px" align="center">

                 

            <tr>

            <td colspan="2">
           <div style="width: 1000px; margin: 0 auto; padding: 0" align="center">
          
            
      <asp:gridview ID="Gridview1" runat="server" ShowFooter="true" 
                    HeaderStyle-BackColor="Green" Width="629px" 
                    OnRowDeleting="Gridview1_RowDeleting"
            AutoGenerateColumns="false">
            <Columns>
            
            <asp:BoundField DataField="RowNumber"  HeaderText="S.No" />
              <asp:CommandField ShowDeleteButton="true" />


              <asp:TemplateField HeaderText="Invoice No">
                <ItemTemplate>
                <asp:TextBox ID="txtinvoiceno" CssClass="TextBox" Width="70px" runat="server"   AutoPostBack="true" onkeypress="return alpha1(event)" OnTextChanged="txtinvoiceno_TextChanged"></asp:TextBox>
                 
                    
                </ItemTemplate>
            </asp:TemplateField>

          

            <asp:TemplateField HeaderText="Invoice Date">
                <ItemTemplate>
                    <asp:TextBox ID="txtinvoicedate"  runat="server" AutoPostBack="true" CssClass="TextBox"  Width="75px" OnTextChanged="txtinvoicedate_TextChanged"></asp:TextBox>
                    
                  </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                <ItemTemplate>
                   <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtinvoicedate"  Format="dd/MM/yyyy" runat="server">
                            </asp:CalendarExtender>

                              <asp:MaskedEditExtender TargetControlID="txtinvoicedate" ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MessageValidatorTip="true"
                                                OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError"
                                                MaskType="Date" CultureName="en-CA"> </asp:MaskedEditExtender>

                            
                </ItemTemplate>
            </asp:TemplateField>

           


           <asp:TemplateField HeaderText="Product Code">
                <ItemTemplate>
                <asp:TextBox ID="txtproductcode" CssClass="TextBox" Width="120px" runat="server"   AutoPostBack="true" onkeypress="return alpha1(event)" OnTextChanged="txtproductcode_TextChanged"></asp:TextBox>
                 <asp:AutoCompleteExtender ID="txtproductcode_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Buyername" ServicePath="" TargetControlID="txtproductcode" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
           </asp:AutoCompleteExtender>
                    
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Product Name">
                <ItemTemplate>
                    <asp:TextBox ID="txtproductname" CssClass="TextBox" Width="120px" runat="server"  AutoPostBack="true" onkeypress="return alpha(event)" OnTextChanged="txtproductname_TextChanged"></asp:TextBox>
                     <asp:AutoCompleteExtender ID="txtproductname_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Buyername1" ServicePath="" TargetControlID="txtproductname" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
           </asp:AutoCompleteExtender>
           </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Batch No">
                <ItemTemplate>
                    <asp:TextBox ID="txtbatchno" CssClass="TextBox" Width="60px" runat="server" AutoPostBack="true"  OnTextChanged="txtbatchno_TextChanged"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Expire Date">
                <ItemTemplate>
                    <asp:TextBox ID="txtexpiredate"  runat="server" AutoPostBack="true" CssClass="TextBox"  Width="75px" OnTextChanged="txtexpiredate_TextChanged"></asp:TextBox>
                    
                  </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                <ItemTemplate>
                   <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtexpiredate" Format="dd/MM/yyyy" runat="server">
                            </asp:CalendarExtender>
                             <asp:MaskedEditExtender TargetControlID="txtexpiredate" ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MessageValidatorTip="true"
                                                OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError"
                                                MaskType="Date" CultureName="en-CA"> </asp:MaskedEditExtender>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Stock In Hand">
                <ItemTemplate>
                    <asp:TextBox ID="txtstockarrival" CssClass="TextBox" Width="50px" AutoPostBack="true" onkeypress="return alpha1(event)"  OnTextChanged="txtstockarrival_TextChanged" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

      
            <asp:TemplateField HeaderText="Free supply">
                <ItemTemplate>
                    <asp:TextBox ID="txtfreesupply" ReadOnly="true" CssClass="TextBox" Width="40px" class="txtMarks" onkeypress="return alpha1(event)" AutoPostBack="true" OnTextChanged="txtfreesupply_TextChanged" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
          
            <asp:TemplateField HeaderText="Tax">
                <ItemTemplate>
                     <asp:TextBox ID="txttax" CssClass="TextBox" Width="40px" class="txtMarks" onkeypress="return alpha1(event)" AutoPostBack="true"  runat="server"></asp:TextBox>
                </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField HeaderText="Purchase Price">
                <ItemTemplate>
                     <asp:TextBox ID="txtpurchaseprice" CssClass="TextBox" Width="60px" runat="server" onkeypress="return alpha1(event)" AutoPostBack="true" OnTextChanged="txtpurchaseprice_TextChanged"></asp:TextBox>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="MRP">
                <ItemTemplate>
                     <asp:TextBox ID="txtMRP" runat="server" CssClass="TextBox" Width="60px" onkeypress="return alpha1(event)" AutoPostBack="true" OnTextChanged="txtMRP_TextChanged"></asp:TextBox>
                     
       
                </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Tax Amount">
                <ItemTemplate>
                     <asp:TextBox ID="txttaxamount" CssClass="TextBox" onkeypress="return alpha1(event)" Width="60px" runat="server"></asp:TextBox>
                </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Product Value">
                <ItemTemplate>
                     <asp:TextBox ID="txtproductvalue" CssClass="TextBox" onkeypress="return alpha1(event)" Width="60px" runat="server"></asp:TextBox>
                </ItemTemplate>

               
              
                
               
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Add" >
                 <ItemTemplate>
               
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add" onclick="ButtonAdd_Click"/>
                 
                </ItemTemplate>
            
                </asp:TemplateField>

                 
            </Columns>
        </asp:gridview>

        <table>

         <tr>
                  <td>
                  <div style="float:right; width:140px">
                  <asp:Label ID="Label7" runat="server" Text="Total Amount:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
                  <asp:Image ID="image5" runat="server" ImageUrl="~/Images/rupees.JPG" />
                  </div>
                 </td>
                  <td>
                   <asp:TextBox ID="txttotalamount"  runat="server"></asp:TextBox>
                  

                  </td>

                  </tr>

                  </table>
         

<br />
<br />
        
  

                 
             



        
         
       
        
    </div>

</td>



            </tr>

        </table>
         <asp:Label ID="lblgroupname" runat="server" Text="" ></asp:Label>
         <asp:Label ID="lblgenericcode" runat="server" Text=""></asp:Label>
         <asp:Label ID="lblchemcode" runat="server" Text="" ></asp:Label>
         <asp:Label ID="lblmedicine" runat="server" Text="" ></asp:Label>
         <asp:Label ID="lblunit" runat="server" Text="" ></asp:Label>
          <asp:Label ID="lblform" runat="server" Text="" ></asp:Label>
          <asp:Label ID="lblmanufacture" runat="server" Text=""></asp:Label>
           <asp:Label ID="lblshelf" runat="server" Text=""></asp:Label>
           <asp:Label ID="lblrock" runat="server" Text=""></asp:Label>
           <asp:Label ID="lblsuplier" runat="server" Text=""></asp:Label>

    
         
              <div align="left" class="SubmitButtons" style="position:relative; left: 450px; top: 0px;"> 
            <asp:Button ID="btnsave" runat="server" Text="Save"  Width="95px" TabIndex="2" CssClass="Buttons embossed-link" OnClick="Button1_Click"/>
              <asp:Button ID="btnExit" runat="server" Text="Exit" Width="95px" CssClass="Buttons embossed-link" OnClick="btnExit_Click" TabIndex="3"/>  
                </div> 

                   </ContentTemplate>
</asp:UpdatePanel> 

           </fieldset>
        
         
         <asp:HiddenField ID="HiddenField" runat="server" />
            <asp:ModalPopupExtender ID="mpeMessagePopup" runat="server" PopupControlID="pnlMessageBox"
               TargetControlID="HiddenField" 
                BackgroundCssClass="modal">
               
            </asp:ModalPopupExtender>
            <asp:UpdatePanel runat="server" ID="pnlMessageBox" Style="display: none;">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnMessagePopupTargetButton" />
            </Triggers>
            <ContentTemplate>
                <div align="center" class="ConfirmBoxHeader">
                    <asp:Label ID="lblMessagePopupHeading" Text="Information" 
                        runat="server" CssClass="Label" align="center"></asp:Label>
               </div>
               <div class="Clear">
        </div>
                <div class="ConfirmBox">
            <div align="center" style="width: 380px; height: 62px; margin: 0 auto;">
                <div style="float: left;">
                    <img src="Images/1389184475_Error.png" alt="" />
                </div>
                <div style="float: left; width: 300px; text-align: center" align="center">
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="Label"  ForeColor="Red" align="center"></asp:Label>
                </div>
            </div>
            <div class="Clear">
            </div>
            <div align="center">
                <asp:Button ID="btnMessagePopupTargetButton" runat="server" Text="OK" CssClass="Buttons embossed-link"
                    />
            </div>
        </div>
            </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

