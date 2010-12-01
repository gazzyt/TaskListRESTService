using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TaskListRESTService.Models
{
	[CollectionDataContract(Name="TaskLists", ItemName="TaskList", Namespace="")]
	public class TaskListsViewModel : List<TaskListViewModel>
	{
	}
}

