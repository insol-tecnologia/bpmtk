﻿using System;
using System.Collections.Generic;
using Bpmtk.Engine.Models;
using Bpmtk.Engine.Storage;
using Bpmtk.Engine.Utils;

namespace Bpmtk.Engine.Tasks
{
    public class TaskInstanceBuilder : ITaskInstanceBuilder
    {
        protected Token token;
        protected string activityId;
        protected string name;
        protected short? priority;
        protected string assignee;
        protected DateTime? dueDate;
        protected IDbSession session;

        public TaskInstanceBuilder(Context context)
        {
            Context = context;
            this.session = context.DbSession;
        }

        public virtual Context Context { get; }


        IContext ITaskInstanceBuilder.Context => this.Context;

        ITaskInstance ITaskInstanceBuilder.Build() => this.Build();

        public virtual TaskInstance Build()
        {
            var date = Clock.Now;

            var task = new TaskInstance();

            //init
            task.IdentityLinks = new List<IdentityLink>();
            task.Variables = new List<Variable>();

            //
            task.Name = this.name;
            task.ActivityId = this.activityId;
            task.Created = date;
            task.State = TaskState.Active;
            task.LastStateTime = date;
            task.Modified = date;
            task.DueDate = this.dueDate;

            if (this.priority.HasValue)
                task.Priority = this.priority.Value;

            task.Assignee = this.assignee;

            if (this.token != null)
            {
                var processInstance = this.token.ProcessInstance;

                task.ActivityInstance = this.token.ActivityInstance;
                task.ProcessInstance = processInstance;
                task.Token = this.token;
                task.ProcessDefinition = processInstance.ProcessDefinition;
            }

            this.session.Save(task);
            this.session.Flush();

            return task;
        }

        public virtual ITaskInstanceBuilder SetActivityId(string activityId)
        {
            this.activityId = activityId;

            return this;
        }

        public virtual ITaskInstanceBuilder SetAssignee(string assignee)
        {
            this.assignee = assignee;

            return this;
        }

        public virtual ITaskInstanceBuilder SetDueDate(DateTime dueDate)
        {
            this.dueDate = dueDate;

            return this;
        }

        public virtual ITaskInstanceBuilder SetName(string name)
        {
            this.name = name;

            return this;
        }

        public virtual ITaskInstanceBuilder SetPriority(short priority)
        {
            this.priority = priority;

            return this;
        }

        public virtual ITaskInstanceBuilder SetToken(Token token)
        {
            this.token = token;

            return this;
        }
    }
}
