<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paypal.aspx.cs" Inherits="Paypal_Integration.paypal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script>
        debugger;
        $.ajax({
            headers: {
                "Accept": "application/json",
                "Accept-Language": "en_US",
                "Authorization": "Basic " + btoa("AQOkOOoFj7m_figyEFedCCj2mnnKxcna7txoaKqpKUGGT8LYlCAlRkyRzqpzPTdsaKDhCw_7XxZX5DsL:EMcd9WB4DCW3YGfmBmyXGJBRKCFwpalI-HHktHecgVNh-TqlWecBUGjivSqfRhtQn3dOKpPneV98kLTY")
            },
            url: "https://api.sandbox.paypal.com/v1/oauth2/token",
            type: "POST",
            data: "grant_type=client_credentials",
            complete: function (result) {

                debugger;
                alert(JSON.stringify(result));
            },
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
