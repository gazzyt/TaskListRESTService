<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Task>" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<h1><%=ViewData.Model.Name%></h1>
	<div>
		<table>
			<tr><td>Description:</td><td><%=ViewData.Model.Description%></td></tr>
			<tr><td>Due:</td><td><%=ViewData.Model.Due%></td></tr>
			<tr><td>Complete:</td><td><%=ViewData.Model.Complete%></td></tr>
		</table>
	</div>
</body>
