﻿using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Managements;
using AdionFA.Infrastructure.Modules.Weka.Model;
using AdionFA.Infrastructure.Weka.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AdionFA.Infrastructure.Helpers
{
    public static class SerializerHelper
    {
        public static void XMLSerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null)
            {
                return;
            }

            try
            {
                var xmlDocument = new XmlDocument();
                var serializer = new XmlSerializer(serializableObject.GetType());
                using var stream = new MemoryStream();

                serializer.Serialize(stream, serializableObject);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(fileName);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public static T XMLDeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return default;
            }

            T objectOut = default;

            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                var xmlString = xmlDocument.OuterXml;

                using var read = new StringReader(xmlString);

                var outType = typeof(T);
                var serializer = new XmlSerializer(outType);
                using var reader = new XmlTextReader(read);
                objectOut = (T)serializer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }

            return objectOut;
        }

        public static void SerializeStrategyNode(string projectName, StrategyNodeModel strategyNode)
        {
            var directory = strategyNode.ParentNodeData.Label.ToLower() == "up"
                ? projectName.ProjectCrossingBuilderNodesUPDirectory()
                : projectName.ProjectCrossingBuilderNodesDOWNDirectory();

            var filename = RegexHelper.GetValidFileName(strategyNode.Name, "_") + ".xml";
            XMLSerializeObject(strategyNode, string.Format(@"{0}\{1}", directory, filename));
        }

        public static void SerializeAssemblyNode(string projectName, AssemblyNodeModel assemblyNode)
        {
            var directory = assemblyNode.ParentNodeData.Label.ToLower() == "up"
                ? projectName.ProjectAssemblyBuilderNodesUPDirectory()
                : projectName.ProjectAssemblyBuilderNodesDOWNDirectory();

            var filename = RegexHelper.GetValidFileName(assemblyNode.Name, "_") + ".xml";
            XMLSerializeObject(assemblyNode, string.Format(@"{0}\{1}", directory, filename));
        }

        public static void SerializeNode(EntityTypeEnum entityType, string projectName, NodeModel node)
        {
            string directory;

            switch (entityType)
            {
                case EntityTypeEnum.StrategyBuilder:
                    directory = node.NodeData.Label.ToLower() == "up"
                        ? projectName.ProjectNodeBuilderNodesUPDirectory()
                        : projectName.ProjectNodeBuilderNodesDOWNDirectory();
                    break;

                case EntityTypeEnum.AssemblyBuilder:
                    directory = node.NodeData.Label.ToLower() == "up"
                        ? projectName.ProjectAssemblyBuilderNodesUPDirectory()
                        : projectName.ProjectAssemblyBuilderNodesDOWNDirectory();
                    break;

                default:
                    return;
            }

            var filename = RegexHelper.GetValidFileName(node.Name, "_") + ".xml";

            XMLSerializeObject(node, string.Format(@"{0}\{1}", directory, filename));
        }
    }

    public class SerializableTuple<T1, T2, T3>
    {
        SerializableTuple() { }

        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }

        public static implicit operator SerializableTuple<T1, T2, T3>(Tuple<T1, T2, T3> t)
        {
            return new SerializableTuple<T1, T2, T3>()
            {
                Item1 = t.Item1,
                Item2 = t.Item2,
                Item3 = t.Item3
            };
        }

        public static implicit operator Tuple<T1, T2, T3>(SerializableTuple<T1, T2, T3> t)
        {
            return Tuple.Create(t.Item1, t.Item2, t.Item3);
        }
    }
}