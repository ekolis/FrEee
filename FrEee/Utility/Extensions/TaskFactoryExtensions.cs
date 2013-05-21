using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace FrEee.Utility.Extensions
{
    public static class TaskFactoryExtensions
    {
        public static Task StartNewWithExceptionHandling(this TaskFactory taskFactory, Action action)
        {
            return taskFactory.StartNew(action).ContinueWith(CheckForExceptions);
        }

        public static Task<TResult> StartNewWithExceptionHandling<TResult>(this TaskFactory taskFactory, Func<TResult> action)
        {
            return taskFactory.StartNew<TResult>(action).ContinueWith<TResult>(CheckForExceptions);
        }

        public static Task StartNewWithExceptionHandling(this TaskFactory taskFactory, Action<object> action, object state)
        {
            return taskFactory.StartNew(action, state).ContinueWith(CheckForExceptions);
        }

        public static Task<TResult> StartNewWithExceptionHandling<TResult>(this TaskFactory taskFactory, Func<object, TResult> action, object state)
        {
            return taskFactory.StartNew<TResult>(action, state).ContinueWith<TResult>(CheckForExceptions);
        }

        public static Task ContinueWithWithExceptionHandling(this Task task, Action<Task> action)
        {
            return task.ContinueWith(CheckForExceptions).ContinueWith(action);
        }

        public static Task ContinueWithWithExceptionHandling(this Task task, Action<Task> action, TaskScheduler scheduler)
        {
            return task.ContinueWith(CheckForExceptions).ContinueWith(action, scheduler);
        }

        public static Task<TResult> ContinueWithWithExceptionHandling<TInput, TResult>(this Task<TInput> task, Func<Task<TInput>, TResult> action)
        {
            return task.ContinueWith<TInput>(CheckForExceptions).ContinueWith(action);
        }

        public static Task<TResult> ContinueWithWithExceptionHandling<TInput, TResult>(this Task<TInput> task, Func<Task<TInput>, TResult> action, TaskScheduler scheduler)
        {
            return task.ContinueWith<TInput>(CheckForExceptions).ContinueWith(action, scheduler);
        }

        public static void ContinueWithWithExceptionHandling<TInput>(this Task<TInput> task, Action<Task<TInput>> action, TaskScheduler scheduler)
        {
            task.ContinueWith<TInput>(CheckForExceptions).ContinueWith(action, scheduler);
        }

        private static void CheckForExceptions(Task task)
        {
            if (task.Exception != null)
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
                {
                    var message = string.Join(Environment.NewLine, task.Exception.InnerExceptions.Select(e => e.Message));
                    throw new Exception(message, task.Exception);
                }));
            }
        }

        private static T CheckForExceptions<T>(Task<T> task)
        {
            if (task.Exception != null)
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
                {
                    var message = string.Join(Environment.NewLine, task.Exception.InnerExceptions.Select(e => e.Message));
                    throw new Exception(message, task.Exception);
                }));
            }

            return task.Result;
        }

        /// <remarks>
        /// Mono support
        /// </remarks>
        public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, Delegate method)
        {
            return dispatcher.BeginInvoke(method, new object[0]);
        }
    }
}
