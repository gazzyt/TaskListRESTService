
using System;
using System.Collections.Generic;

using NHibernate;
using NHibernate.Criterion;

using TaskListDao;
using TaskListDao.Model;

namespace TaskListDaoHibernateImpl
{
	
	
	public class TaskListDaoHibernateImpl : ITaskListDao
	{
		private NHibernateHelper _nhibernateHelper;

		#region ITaskListDao implementation
		public System.Collections.Generic.IEnumerable<TaskListDao.Model.TaskList> GetTaskLists ()
		{
			ISession session = _nhibernateHelper.OpenSession();

			return session
				.CreateCriteria<TaskList>()
					.List<TaskList>();
		}
		
		public void AddTaskList (TaskListDao.Model.TaskList newList)
		{
			ISession session = _nhibernateHelper.OpenSession();

			session.Save(newList);
			session.Flush();
		}
		
		public TaskList GetTaskList(Guid id)
		{
			ISession session = _nhibernateHelper.OpenSession();

			return session.Get<TaskList>(id);
		}
		
		public void UpdateTaskList(TaskList taskList)
		{
			ISession session = _nhibernateHelper.OpenSession();
			
			TaskList originalTaskList = session.Get<TaskList>(taskList.Id);
			originalTaskList.Name = taskList.Name;
			
			session.Update(originalTaskList);
			session.Flush();
		}

		public void DeleteTaskList(Guid taskListId)
		{
			ISession session = _nhibernateHelper.OpenSession();
			TaskList taskList = session.Get<TaskList>(taskListId);
			session.Delete(taskList);
			session.Flush();
		}
		
		public void DeleteAllTaskLists()
		{
			ISession session = _nhibernateHelper.OpenSession();
			session.Delete("from TaskList t");
			session.Flush();
		}

		public IEnumerable<Task> GetTasksInList(Guid taskListId)
		{
			ISession session = _nhibernateHelper.OpenSession();

			return session
				.CreateCriteria<Task>()
					.Add(Expression.Eq("TaskListId", taskListId))
					.List<Task>();
		}
		
		public void AddTask(Task newTask)
		{
			ISession session = _nhibernateHelper.OpenSession();

			session.Save(newTask);
			session.Flush();
		}

		public Task GetTask(Guid taskId)
		{
			ISession session = _nhibernateHelper.OpenSession();
			
			Task task = session.Get<Task>(taskId);
			
			return task;
		}

		public void UpdateTask(Task task)
		{
			ISession session = _nhibernateHelper.OpenSession();
			
			session.Update(task);
			session.Flush();
		}

		public void DeleteTask(Guid taskId)
		{
			ISession session = _nhibernateHelper.OpenSession();
			Task task = session.Get<Task>(taskId);
			session.Delete(task);
			session.Flush();
		}
		#endregion
		
		public TaskListDaoHibernateImpl(NHibernateHelper nhibernateHelper)
		{
			_nhibernateHelper = nhibernateHelper;
		}
	}
}
