﻿using System;
using System.Collections.Generic;
using Bpmtk.Infrastructure;
using Bpmtk.Engine.Models;

namespace Bpmtk.Engine.Repository
{
    public class Package : IAggregateRoot
    {
        //protected Model model;
        //protected List<PackageResourceLink> resourceLinks = new List<PackageResourceLink>();

        //protected Package()
        //{
        //}

        //public Package(string key, 
        //    int ownerId, 
        //    int version = 1,
        //    byte[] modelBytes = null)
        //{
        //    this.Key = key;
        //    this.OwnerId = ownerId;
        //    this.Version = version;
        //    this.Created = DateTime.Now;
        //    this.Modified = this.Created;
        //    this.model = new Model() { Content = modelBytes };
        //}

        public virtual int Id
        {
            get;
            protected set;
        }

        public virtual string TenantId
        {
            get;
            set;
        }

        public virtual string Category
        {
            get;
            set;
        }

        public virtual User Owner
        {
            get;
            protected set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual string Description
        {
            get;
            set;
        }

        public virtual int Version
        {
            get;
            protected set;
        }

        /// <summary>
        /// 并发控制列
        /// </summary>
        public virtual string ConcurrencyStamp
        {
            get;
            set;
        }

        //public virtual IReadOnlyCollection<PackageResourceLink> ResourceLinks
        //{
        //    get => this.resourceLinks.AsReadOnly();
        //}

        public virtual void IncreaseVersion()
        {
            this.Version += 1;
            this.Modified = DateTime.Now;
            this.ConcurrencyStamp = Guid.NewGuid().ToString("n");
        }

        public virtual DateTime Created
        {
            get;
            protected set;
        }

        public virtual DateTime Modified
        {
            get;
            set;
        }

        public virtual ByteArray Source
        {
            get;
            set;
        }

        //public virtual Model Model
        //{
        //    get => this.model;
        //}
    }
}
