<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Productinward.aspx.cs" Inherits="_Default" %>
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
           return ((k > 48 && k < 57) || (k > 96 && k < 123) || (k > 64 && k < 91) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39 );   //k=9(keycode for tab) ||
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

   


 

 <fieldset  class="BigFieldSet">
    <legend class="BigLegend">
    <h1 style="position: relative;">
     Purchase
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

   
          
    <div align="right">
      
       <asp:Label ID="Label2" runat="server" Text="Balance Amount:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
        <asp:Image ID="image2" runat="server" ImageUrl="~/Images/rupees.JPG" />
        
              
                   <asp:TextBox ID="txtadjustamount" ReadOnly="true" runat="server" 
            width="80px"></asp:TextBox>

         
                  
    
    </div>
   

   
     <table id="Table2" cellpadding="10" runat="server" style="border: solid 15px Green; background-color: SkyBlue;"  cellspacing="10"  width="50%" align="center">
      <tr>
            <td align="center">
                <span style="color: Red; font-weight: bold; font-size: 18pt;"></span>&nbsp;
                <asp:Label ID="lblmod" runat="server" Text="lblmodsuccess" Font-Bold="true" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                </tr>
         </table>
      <table id="Table1" align="center">
      <tr>

      <td class="style12">
      </td>
      
      
      </tr>
      <tr>
       <td colspan="6" align="center">
       
                          <asp:Label ID="lblGcode" runat="server" Text="Payment Type:"  Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
                          <asp:DropDownList ID="ddpaymenttype" runat="server" height="25px"  
                              CssClass="DropDown" AutoPostBack="true" 
                              onselectedindexchanged="ddpaymenttype_SelectedIndexChanged">
                           <asp:ListItem Value="10" Text="Credit"> </asp:ListItem>
                           <asp:ListItem Value="11" Text="Cash"> </asp:ListItem>
                          </asp:DropDownList>
                          
                     </td>
                    
 </tr>

                      <tr>
                    <td>
                    <div style="float:right; width:88px">
                        <asp:Label ID="lblProd" runat="server" Text="Invoice No:" Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                        </div>
                        </td>
                        
                           <td align="left">
                               <asp:TextBox ID="txtinvoiceno" runat="server" width="80px"  CssClass="TextBox" onkeydown="return validate(event,this);"></asp:TextBox>
                               
                               <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                              
                            </td>

                           
                             
                            
                          
                            <td>
                            <div style="float:right; width:100px">
                            <asp:Label ID="lblPname" runat="server" Text="Invoice Date:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
                            </div>
                            </td>
                            <td class="style13">
                            <asp:TextBox ID="txtinvoicedate" runat="server"  CssClass="TextBox" Width="80px" AutoPostBack="true"
                                     onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)" 
                                    ontextchanged="txtinvoicedate_TextChanged"></asp:TextBox>
                                      <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtinvoicedate" Format="dd/MM/yyyy" runat="server">
                            </asp:CalendarExtender>
                             <asp:MaskedEditExtender TargetControlID="txtinvoicedate" ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MessageValidatorTip="true"
                                                OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError"
                                                MaskType="Date" CultureName="en-CA"> </asp:MaskedEditExtender>
              <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                            </td>
                           
                            <td>
                                                       <asp:Label ID="Label1" runat="server" CssClass="Label" Font-Bold="true" 
                                                           Font-Size="Medium" Text="Invoice Amount:"></asp:Label>
                                                       
                                                       <asp:Image ID="image1" runat="server" ImageUrl="~/Images/rupees.JPG" />
                                                   </td>
                                                   <td>
                                                  
                                                   
                                                      <asp:TextBox ID="txtinvoiceamount" runat="server"  CssClass="TextBox" Width="100px" AutoPostBack="true"
                                      onkeypress="return alpha1(event)" onkeydown="return validate(event,this);"
                                                           ontextchanged="txtinvoiceamount_TextChanged"></asp:TextBox>

                                     <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                                      
                                      
                                                   </td>
                            
                          </tr>
                           <tr>
                    <td>
                   
                        <asp:CheckBox ID="chkpayment"  runat="server"  CssClass="CheckBox" Text="With Supplier" autopostback="true" onkeydown="return(event.keyCode != 13)" 
                            oncheckedchanged="chkpayment_CheckedChanged"/>
                             
                    </td>
                    <td></td>
                              
                                   <td>
                                   <div style="float:right; width:118px">
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
                                                           CssClass="DropDown" onselectedindexchanged="ddlsupplier_SelectedIndexChanged" 
                                                           Width="150px">
                                                       </asp:DropDownList>
                                                       </ContentTemplate>
</asp:UpdatePanel>
                                                  
                                   <asp:Label ID="lblsuppliercode" runat="server" Text="" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
                              </td>

                              <td>
                              <div style="float:right; width:40px">
                                                       <asp:Label ID="Label9" runat="server" CssClass="Label"  Font-Bold="true" 
                                                           Font-Size="Medium" Text="Date:"  ></asp:Label>
                                                           </div>
                                                   </td>
                                                   <td>
                                                      <asp:TextBox ID="txtdate" runat="server" Width="80px" AutoPostBack="true" 
                                     onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)" CssClass="TextBox"></asp:TextBox>
                                     <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                                  </td>
                                                    

                                                    </tr>

                                                   

                                                  


                                           
                                          
                               
                              
                              
                              
                   
                          <tr >
                         
                              <td class="style12">
                                   <asp:Label ID="lblgrcode" runat="server" Text="" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
                              </td>
                               <td>
                                   <asp:Label ID="lblpfflag" runat="server" Text="" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
                              </td>
                             
                          </tr>
                         </table>
                        <div>

   

  <table width="800px" align="center">

                 

            <tr>

            <td colspan="2">
           <div style="width: 980px; margin: 0 auto; padding: 0" align="center">

          
            
      <asp:gridview ID="Gridview1" runat="server" ShowFooter="true" 
                    HeaderStyle-BackColor="Green" Width="629px"
            AutoGenerateColumns="false" onrowcreated="Gridview1_RowCreated"  
                    OnRowDataBound="Gridview1_RowDataBound"
                       OnRowDeleting="Gridview1_RowDeleting">
            <Columns>
            
            <asp:BoundField DataField="RowNumber" HeaderText="Serial No" />
              <asp:CommandField ShowDeleteButton="true" />
             
            <asp:TemplateField HeaderText="Product Code">
                <ItemTemplate>
                <asp:TextBox ID="txtproductcode" CssClass="TextBox" Width="120px" runat="server"   AutoPostBack="true" onkeypress="return alpha1(event)" OnTextChanged="txtproductcode_TextChanged" TabIndex="1"></asp:TextBox>
                 <asp:AutoCompleteExtender ID="txtproductcode_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Buyername" ServicePath="" TargetControlID="txtproductcode" CompletionListItemCssClass="OtherCompletionItemCssClass"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
           </asp:AutoCompleteExtender>
                    
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Product Name">
                <ItemTemplate>
                    <asp:TextBox ID="txtproductname" CssClass="TextBox" Width="120px" runat="server"  AutoPostBack="true" onkeypress="return alpha(event)" OnTextChanged="txtproductname_TextChanged" TabIndex="2"></asp:TextBox>
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
                    <asp:TextBox ID="txtexpiredate"  runat="server" AutoPostBack="true" CssClass="TextBox" OnTextChanged="txtexpiredate_TextChanged" Width="75px"></asp:TextBox>
                    
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
                <asp:TemplateField HeaderText="Stock Arrival">
                <ItemTemplate>
                    <asp:TextBox ID="txtstockarrival" CssClass="TextBox" Width="50px" AutoPostBack="true" onkeypress="return alpha1(event)" OnTextChanged="txtstockarrival_TextChanged"  runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

      
            <asp:TemplateField HeaderText="Free supply">
                <ItemTemplate>
                    <asp:TextBox ID="txtfreesupply" CssClass="TextBox" Width="40px" class="txtMarks" onkeypress="return alpha1(event)" AutoPostBack="true" OnTextChanged="txtfreesupply_TextChanged" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
          
            <asp:TemplateField HeaderText="Tax">
                <ItemTemplate>
                     <asp:TextBox ID="txttax" CssClass="TextBox" Width="40px" class="txtMarks" onkeypress="return alpha1(event)" AutoPostBack="true" OnTextChanged="txttax_TextChanged" runat="server"></asp:TextBox>
                </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField HeaderText="Purchase Price">
                <ItemTemplate>
                     <asp:TextBox ID="txtpurchaseprice" CssClass="TextBox" Width="60px" runat="server" onkeypress="return alpha1(event)" AutoPostBack="true" OnTextChanged="txtpurchaseprice_TextChanged"></asp:TextBox>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="MRP">
                <ItemTemplate>
                     <asp:TextBox ID="txtMRP" runat="server" CssClass="TextBox" Width="60px" onkeypress="return alpha1(event)" AutoPostBack="true" OnTextChanged="txtMRP_TextChanged" ></asp:TextBox>
                     
       
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

                  <asp:TemplateField HeaderText="Group Name">
                <ItemTemplate>
                     <asp:TextBox ID="txtgroupname" ReadOnly="true" CssClass="TextBox" onkeypress="return alpha1(event)" Width="60px"  runat="server"></asp:TextBox>
                </ItemTemplate>
                
               
              
                
               
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Add">
                 <ItemTemplate>
               
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add"  onclick="ButtonAdd_Click"/>
                 
                </ItemTemplate>
            
                </asp:TemplateField>
            </Columns>
        </asp:gridview>


     

<br />
<br />
        <div align="left">

         <asp:Label ID="Label10" runat="server" Text="Narrations:"  TextMode="MultiLine" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
                   <asp:TextBox ID="txtnarrations" TextMode="MultiLine" runat="server" AutoPostBack="true"  ontextchanged="txtnarrations_TextChanged"></asp:TextBox>
                 
                    
        
        </div>

        <div>

        <asp:TextBox ID="txtamount"  runat="server" AutoPostBack="true" Visible="false"></asp:TextBox>
        
        </div>

  

                 
              <table align="right">
                 <tr>
                   <td>
              <div style="float:right; width:88px">
              <asp:Label ID="Label5" runat="server" Text="Others:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
              <asp:Image ID="image3" runat="server" ImageUrl="~/Images/rupees.JPG" />
              </div>
              </td>
              <td>
              <asp:TextBox ID="txtothers" Width="120px"  runat="server" AutoPostBack="true" OnTextChanged="txtothers_TextChanged" ></asp:TextBox>
                  
                     </td>
                  </tr>

                  <tr>
                  <td>
                  <div style="float:right; width:110px">
                  <asp:Label ID="Label11" runat="server" Text="RoundOff:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
                  <asp:Image ID="image4" runat="server" ImageUrl="~/Images/rupees.JPG" />
                  </div>
                  </td>
                   <td>
                   <asp:TextBox ID="txtroundoff" Width="120px"  runat="server" AutoPostBack="true" OnTextChanged="txtroundoff_TextChanged"></asp:TextBox>
                  

                  </td>

                  </tr>

                  <tr>
                  <td>
                  <div style="float:right; width:140px">
                  <asp:Label ID="Label7" runat="server" Text="Total Amount:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
                  <asp:Image ID="image5" runat="server" ImageUrl="~/Images/rupees.JPG" />
                  </div>
                 </td>
                  <td>
                   <asp:TextBox ID="txttotalamount" Width="120px" ReadOnly="true"  runat="server"></asp:TextBox>
                  

                  </td>

                  </tr>
             </table>



        
         
       
        
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

    </div>
   
         
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
                  OnClick="btnMessagePopupTargetButton_Click"  />
            </div>
        </div>
            </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

