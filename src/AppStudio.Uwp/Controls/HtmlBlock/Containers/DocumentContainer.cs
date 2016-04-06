﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace AppStudio.Uwp.Controls.Html.Containers
{
    abstract class DocumentContainer
    {
        public DocumentContainer Parent { get; private set; }

        public abstract void Add(DependencyObject ctrl);
        public abstract bool CanAdd(DependencyObject ctrl);

        public DocumentContainer Create(DependencyObject ctrl)
        {
            DocumentContainer container = null;
            if (ctrl is Grid)
            {
                container = new GridDocumentContainer(ctrl as Grid);
            }
            else if (ctrl is Paragraph)
            {
                container = new ParagraphDocumentContainer(ctrl as Paragraph);
            }
            else if (ctrl is Span)
            {
                container = new SpanDocumentContainer(ctrl as Span);
            }
            if (container != null)
            {
                container.Parent = this;
            }

            return container;
        }

        public DocumentContainer Find(DependencyObject ctrl)
        {
            var c = this;

            while (c.Parent != null)
            {
                if (c.CanAdd(ctrl))
                {
                    return c;
                }
                c = c.Parent;
            }
            return null;
        }
    }

    abstract class DocumentContainer<T> : DocumentContainer where T : DependencyObject
    {
        public T Control { get; private set; }

        public DocumentContainer(T ctrl)
        {
            Control = ctrl;
        }
    }
}