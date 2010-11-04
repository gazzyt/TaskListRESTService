<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Task>>" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<h1>Tasks</h1>
	<div>
	<table>
		<% foreach (var task in ViewData.Model) { %>
		<tr>
			<td><%=task.Id.ToString()%></td>
			<td><%=task.Name%></td>
		</tr>
		<% } %>
	</table>
	</div>
</body>
