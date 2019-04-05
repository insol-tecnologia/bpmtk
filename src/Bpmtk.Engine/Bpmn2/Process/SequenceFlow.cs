﻿using System;
using System.Collections.Generic;
using Bpmtk.Engine.Bpmn2.Extensions;
using Bpmtk.Engine.Runtime;
using Bpmtk.Engine.Stores;

namespace Bpmtk.Engine.Bpmn2
{
    public class SequenceFlow : FlowElement
    {
        public virtual Expression ConditionExpression
        {
            get;
            set;
        }

        protected List<EventListener> eventListeners = new List<EventListener>();

        public virtual IList<EventListener> EventListeners => this.eventListeners;

        public virtual FlowNode SourceRef
        {
            get;
            set;
        }

        public virtual FlowNode TargetRef
        {
            get;
            set;
        }

        public bool? IsImmediate
        {
            get;
            set;
        }

        public override string ToString()
        {
            return $"{this.Id}, {this.Name}";
        }

        public virtual void Take(ExecutionContext executionContext)
        {
            // update the runtime context information
            Token token = executionContext.Token;
            token.Node = null;

            // start the transition log
            //TransitionLog transitionLog = new TransitionLog(this,
            //  executionContext.getTransitionSource());
            //token.startCompositeLog(transitionLog);
            //try
            //{
            //    // fire leave events for superstates (if any)
            //    fireSuperStateLeaveEvents(executionContext);

            //    // fire the transition event (if any)
            //    fireEvent(Event.EVENTTYPE_TRANSITION, executionContext);
            var store = executionContext.Context.GetService<IProcessInstanceStore>();
            store.Add(new HistoricToken(executionContext, "transition"));
            //    // fire enter events for superstates (if any)
            //    Node destination = fireSuperStateEnterEvents(executionContext);
            //    // update the ultimate destinationNode of this transition
            //    transitionLog.setDestinationNode(destination);
            //}
            //finally
            //{
            //    // end the transition log
            //    token.endCompositeLog();
            //}

            // pass the token to the destinationNode node
            this.TargetRef.Enter(executionContext);
        }
    }
}