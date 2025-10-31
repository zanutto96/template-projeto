using Gerador.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gerador
{
   public static class Util
   {
      public static void WriteDocument(string Template, string DestinyFile, TableToGenerate tb)
      {
         StreamWriter xfile = new StreamWriter(DestinyFile, false, Encoding.UTF8);
         string? line;
         System.IO.StreamReader template = new System.IO.StreamReader(Template);
         while ((line = template.ReadLine()) != null)
         {
            if (line.Contains("#Entity#"))
            {
               line = line.Replace("#Entity#", tb.TableName);
            }
            if (line.Contains("#entity#"))
            {
               line = line.Replace("#entity#", tb.TableName.ToLower());
            }
            if (line.Contains("#EntityLowerCase#"))
            {
               line = line.Replace("#EntityLowerCase#", tb.TableName.ToLower());
            }
            if (line.Contains("#EntityProperties#"))
            {
               line = "";
               var columns = GetEntityProperties(tb);
               foreach (var column in columns)
               {
                  line += column;
               }
            }
            if (line.Contains("#EntityBasicFilters#"))
            {
               line = "";
               var columns = GetEntityBasicFilters(tb);
               foreach (var column in columns)
               {
                  line += column;
               }
            }
            if (line.Contains("#EntityFormControls#"))
            {
               line = "";
               var columns = GetEntityFormControls(tb);
               foreach (var column in columns)
               {
                  line += column;
               }
            }

            if (line.Contains("#EntityConfiguratioPrimaryKey#"))
            {
               var primaryKey = tb.ColumList.Where(_ => _.IsPrimaryKey == true).FirstOrDefault();
               if (primaryKey != null)
               {
                  line = line.Replace("#EntityConfiguratioPrimaryKey#", $"builder.HasKey(p => p.{primaryKey.Name});");
               }
            }
            if (line.Contains("#EntityConfiguratioRelations#"))
            {
               line = "";
               var relations = GetEntityConfiguratioRelations(tb);
               foreach (var relation in relations)
               {
                  line += relation;
               }
            }
            if (line.Contains("#EntityModelProperties#"))
            {
               line = "";
               var columns = GetEntityModelProperties(tb);
               foreach (var column in columns)
               {
                  line += column;
               }
            }
            if (line.Contains("#EntityHtmlFormFields#"))
            {
               line = "";
               var fields = GetEntityHtmlFormFields(tb);
               foreach (var field in fields)
               {
                  line += field;
               }
            }
            if (line.Contains("#EntityHtmlMaterialProperties#"))
            {
               line = "";
               var columns = GetEntityHtmlMaterialProperties(tb);
               foreach (var column in columns)
               {
                  line += column;
               }
            }
            if (line.Contains("#EntityHtmlTableHeaderProperties#"))
            {
               line = "";
               var columns = GetEntityHtmlTableHeaderProperties(tb);
               foreach (var column in columns)
               {
                  line += column;
               }
            }
            if (line.Contains("#EntityHtmlTableHeaderFilterMaterialProperties#"))
            {
               line = "";
               var columns = GetEntityHtmlTableHeaderFilterMaterialProperties(tb);
               foreach (var column in columns)
               {
                  line += column;
               }
            }
            if (line.Contains("#EntityHtmlTableHeaderMaterialProperties#"))
            {
               line = "";
               var columns = GetEntityHtmlTableHeaderMaterialProperties(tb);
               foreach (var column in columns)
               {
                  line += column;
               }
            }
            if (line.Contains("#EntityTableColumnsConfig#"))
            {
               line = "";
               var columns = GetEntityTableColumnsConfig(tb);
               foreach (var column in columns)
               {
                  line += column;
               }
            }
            if (line.Contains("#EntityHtmlTableRowProperties#"))
            {
               line = "";
               var columns = GetEntityHtmlTableRowProperties(tb);
               foreach (var column in columns)
               {
                  line += column;
               }
            }

            if (line.Contains("#EntityRepositoryPrimaryKey#"))
            {
               var primaryKey = tb.ColumList.Where(_ => _.IsPrimaryKey == true).FirstOrDefault();
               if (primaryKey != null)
               {
                  line = line.Replace("#EntityRepositoryPrimaryKey#", primaryKey.Name);
               }
            }

            if (line.Contains("#EntityRepositoryFirstString#"))
            {
               var primaryKey = tb.ColumList.Where(column => column.Type.ToLower() == "char" || column.Type.ToLower() == "varchar" || column.Type.ToLower() == "nvarchar").FirstOrDefault();
               if (primaryKey != null)
               {
                  line = line.Replace("#EntityRepositoryFirstString#", primaryKey.Name);
               }
               else
               {
                  primaryKey = tb.ColumList.Where(_ => _.IsPrimaryKey == true).FirstOrDefault();
                  if (primaryKey != null)
                  {
                     line = line.Replace("#EntityRepositoryFirstString#", primaryKey.Name);
                  }
               }
            }


            if (line.Contains("#EntityRepositoryPrimaryKeyFront#"))
            {
               var primaryKey = tb.ColumList.Where(_ => _.IsPrimaryKey == true).FirstOrDefault();
               if (primaryKey != null)
               {
                  var name = primaryKey.Name.Substring(0, 1).ToLower() + primaryKey.Name.Substring(1);
                  line = line.Replace("#EntityRepositoryPrimaryKeyFront#", name);
               }
            }

            xfile.WriteLine(line);
         }
         xfile.Close();
      }

      public static List<string> GetEntityHtmlTableRowProperties(TableToGenerate tb)
      {
         List<string> data = new List<string>();
         foreach (var column in tb.ColumList)
         {
            string c = "              <td>{{item." + column.Name.FirstCharToLowerCase() + "}}</td>\r\n";
            data.Add(c);
         }
         return data;
      }

      public static List<string> GetEntityHtmlTableHeaderProperties(TableToGenerate tb)
      {
         List<string> data = new List<string>();
         foreach (var column in tb.ColumList)
         {
            string c = $"              <th>{column.Name}</th>\r\n";
            data.Add(c);
         }
         return data;
      }

      public static List<string> GetEntityHtmlTableHeaderFilterMaterialProperties(TableToGenerate tb)
      {
         List<string> data = new List<string>();
         foreach (var column in tb.ColumList)
         {
            string c = $"     <ng-container matColumnDef=\"filtro_{column.Name.FirstCharToLowerCase()}\">\r\n";
            c += $"                 <th mat-header-cell *matHeaderCellDef class=\"bg-slate-50\">\r\n";
            c += $"                    <mat-form-field class=\"mt-1 mb-1 w-100\">\r\n";
            c += $"                       <input matInput type=\"text\" [(ngModel)]=\"filters['{column.Name.FirstCharToLowerCase()}']\" (ngModelChange)=\"onFilter(filters)\" /> \r\n";
            c += $"                    </mat-form-field> </th>\r\n";
            c += $"          </ng-container>\r\n";
            data.Add(c);
         }
         return data;
      }
      public static List<string> GetEntityHtmlTableHeaderMaterialProperties(TableToGenerate tb)
      {
         List<string> data = new List<string>();
         foreach (var column in tb.ColumList)
         {
            string c = $"<ng-container matColumnDef=\"{column.Name.FirstCharToLowerCase()}\">\r\n";
            c += $"        <th mat-header-cell mat-sort-header *matHeaderCellDef class=\"bg-slate-100 text-sm\"> {column.Name} </th>\r\n";
            c += $"        <td mat-cell *matCellDef=\"let element\"> {"{{element." + column.Name.FirstCharToLowerCase() + "}}"} </td>\r\n";
            c += $"     </ng-container>\r\n";
            data.Add(c);
         }
         return data;
      }

      public static string GetEntityHtmlDisplayedColumnsMaterialProperties(TableToGenerate tb)
      {
         var data = "    displayedColumns: string[] = [ ";
         foreach (var column in tb.ColumList)
         {
            data += "'" + column.Name.FirstCharToLowerCase() + "', ";
         }
         return data + " 'buttons' ]";
      }

      public static List<string> GetEntityFormControls(TableToGenerate tb)
      {
         List<string> data = new List<string>();
         foreach (var column in tb.ColumList)
         {
            string c = $"      {column.Name.FirstCharToLowerCase()}: new FormControl(),\r\n";
            data.Add(c);
         }
         return data;
      }

      public static List<string> GetEntityHtmlBootstrapFormFields(TableToGenerate tb)
      {
         List<string> data = new List<string>();

         foreach (var column in tb.ColumList)
         {
            if (!column.IsPrimaryKey)
            {
               string c = $"        <div class=\"form-group row\">\r\n";
               c += $"          <label class=\"col-sm-3 col-form-label\" for=\"{column.Name.FirstCharToLowerCase()}\">{column.Name}</label>\r\n";
               c += $"          <div class=\"col-sm-9\">\r\n";

               string required = column.IsNullable ? "" : "required";
               string placeholder = $"Digite o {column.Name.FirstCharToLowerCase()}";

               if (column.Type.ToLower() == "bit" || column.Type.ToLower() == "boolean" || column.Type.ToLower() == "bool" || column.Type.ToLower() == "tinyint")
               {
                  c += $"            <div class=\"form-check\">\r\n";
                  c += $"              <input class=\"form-check-input\" type=\"checkbox\" formControlName=\"{column.Name.FirstCharToLowerCase()}\" {required} name=\"{column.Name.FirstCharToLowerCase()}\" [(ngModel)]=\"model.{column.Name.FirstCharToLowerCase()}\" id=\"{column.Name.FirstCharToLowerCase()}\">\r\n";
                  c += $"              <label class=\"form-check-label\" for=\"{column.Name.FirstCharToLowerCase()}\">{column.Name}</label>\r\n";
                  c += $"            </div>\r\n";
               }
               else if (column.Type.ToLower() == "date" || column.Type.ToLower() == "datetime")
               {
                  c += $"            <input type=\"datetime-local\" class=\"form-control\" formControlName=\"{column.Name.FirstCharToLowerCase()}\" {required} name=\"{column.Name.FirstCharToLowerCase()}\" [(ngModel)]=\"model.{column.Name.FirstCharToLowerCase()}\" id=\"{column.Name.FirstCharToLowerCase()}\" placeholder=\"{placeholder}\">\r\n";
               }
               else if (column.IsForeignKey || (column.Type.ToLower() == "int" && !column.IsPrimaryKey))
               {
                  var dataItem = column.Name;
                  if (column.Name.ToLower().StartsWith("id"))
                  {
                     dataItem = column.Name.Substring(2);
                  }
                  else if (column.Name.ToLower().EndsWith("id"))
                  {
                     dataItem = column.Name.Substring(0, column.Name.Length - 2);
                  }
                  c += $"            <custom-select\r\n";
                  c += $"              formControlName=\"{column.Name.FirstCharToLowerCase()}\"\r\n";
                  c += $"              name=\"{column.Name.FirstCharToLowerCase()}\"\r\n";
                  c += $"              tableName=\"{dataItem}\"\r\n";
                  c += $"              tableLabelFieldName=\"name\"\r\n";
                  c += $"              [(ngModel)]=\"model.{column.Name.FirstCharToLowerCase()}\"\r\n";
                  c += $"              id=\"{column.Name.FirstCharToLowerCase()}\"\r\n";
                  c += $"              placeholder=\"Selecione...\">\r\n";
                  c += $"            </custom-select>\r\n";
               }
               else
               {
                  string inputType = "text";
                  if (column.Type.ToLower() == "int" || column.Type.ToLower() == "bigint" || column.Type.ToLower() == "smallint" ||
                      column.Type.ToLower() == "decimal" || column.Type.ToLower() == "numeric" || column.Type.ToLower() == "float" ||
                      column.Type.ToLower() == "double" || column.Type.ToLower() == "real")
                  {
                     inputType = "number";
                  }

                  c += $"            <input type=\"{inputType}\" class=\"form-control\" formControlName=\"{column.Name.FirstCharToLowerCase()}\" {required} name=\"{column.Name.FirstCharToLowerCase()}\" [(ngModel)]=\"model.{column.Name.FirstCharToLowerCase()}\" id=\"{column.Name.FirstCharToLowerCase()}\" placeholder=\"{placeholder}\">\r\n";
               }

               c += $"            <small class=\"form-text text-muted\">Digite uma descrição clara e objetiva</small>\r\n";
               c += $"          </div>\r\n";
               c += $"        </div>\r\n";

               data.Add(c);
            }
         }

         return data;
      }

      public static List<string> GetEntityHtmlMaterialProperties(TableToGenerate tb)
      {
         List<string> data = new List<string>();

         foreach (var column in tb.ColumList)
         {
            string c = $"        <mat-form-field class=\"w-full mt-3\">\r\n";
            //c += $"          <div class=\"form-group\">\r\n";
            string required = column.IsNullable ? "" : "required";
            if ((column.Type.ToLower() == "char" || column.Type.ToLower() == "varchar" || column.Type.ToLower() == "nvarchar"))
            {
               c += $"            <mat-label class=\"font-semibold\">{column.Name}</mat-label>\r\n";
               c += $"            <input matInput type=\"text\" name=\"{column.Name.FirstCharToLowerCase()}\" {required} [(ngModel)]=\"model.{column.Name.FirstCharToLowerCase()}\" formControlName=\"{column.Name.FirstCharToLowerCase()}\" />\r\n";
            }

            if (column.Type.ToLower() == "date" || column.Type.ToLower() == "datetime")
            {
               c += $"            <mat-label class=\"font-semibold\">{column.Name}</mat-label>\r\n";
               c += $"            <input matInput type=\"datetime-local\" formControlName=\"{column.Name.FirstCharToLowerCase()}\" {required} name=\"{column.Name.FirstCharToLowerCase()}\" [value]=\"model.{column.Name.FirstCharToLowerCase()}\" [(ngModel)]=\"model.{column.Name.FirstCharToLowerCase()}\" />\r\n";
            }
            if (column.Type.ToLower() == "bit")
            {
                    c = "<div class=\"w-full \">\r\n";
                c += $"<mat-checkbox formControlName=\"{column.Name.FirstCharToLowerCase()}\" name=\"{column.Name.FirstCharToLowerCase()}\" [value]=\"model.{column.Name.FirstCharToLowerCase()}\" [(ngModel)]=\"model.{column.Name.FirstCharToLowerCase()}\">{column.Name}</mat-checkbox>\r\n";
                    c += "</div>\r\n";
                    data.Add(c);
                    continue;
            }
            if (column.IsForeignKey)
            {
               var dataItem = column.Name;
               if (column.Name.ToLower().StartsWith("id"))
               {
                  dataItem = column.Name.Substring(2);
               }
               else if (column.Name.ToLower().EndsWith("id"))
               {
                  dataItem = column.Name.Substring(0, column.Name.Length - 2);
               }
               c += $"            <mat-label class=\"font-semibold\">{column.Name}</mat-label>\r\n";
               c += $"            <custom-select\r\n";
               c += $"              formControlName=\"{column.Name.FirstCharToLowerCase()}\"\r\n";
               c += $"              name=\"{column.Name.FirstCharToLowerCase()}\"\r\n";
               c += $"              tableName=\"{dataItem}\"\r\n";
               c += $"              tableLabelFieldName=\"name\"\r\n";
               c += $"              [(ngModel)]=\"model.{column.Name.FirstCharToLowerCase()}\"\r\n";
               c += $"              id=\"{column.Name.FirstCharToLowerCase()}\"\r\n";
               c += $"              placeholder=\"Selecione...\">\r\n";
               c += $"            </custom-select>\r\n";
            }
            if (!column.IsPrimaryKey && column.Type.ToLower() == "int" && !column.IsForeignKey)
            {
               if (column.Name.ToLower().StartsWith("id"))
               {
                  var dataItem = column.Name.Substring(2);
                  c += $"            <mat-label class=\"font-semibold\">{column.Name}</mat-label>\r\n";
                  c += $"            <custom-select\r\n";
                  c += $"              formControlName=\"{column.Name.FirstCharToLowerCase()}\"\r\n";
                  c += $"              name=\"{column.Name.FirstCharToLowerCase()}\"\r\n";
                  c += $"              tableName=\"{dataItem}\"\r\n";
                  c += $"              tableLabelFieldName=\"name\"\r\n";
                  c += $"              [(ngModel)]=\"model.{column.Name.FirstCharToLowerCase()}\"\r\n";
                  c += $"              id=\"{column.Name.FirstCharToLowerCase()}\"\r\n";
                  c += $"              placeholder=\"Selecione...\">\r\n";
                  c += $"            </custom-select>\r\n";
               }
               else if (column.Name.ToLower().EndsWith("id"))
               {
                  var dataItem = column.Name.Substring(0, column.Name.Length - 2);
                  c += $"            <mat-label class=\"font-semibold\">{column.Name}</mat-label>\r\n";
                  c += $"            <custom-select\r\n";
                  c += $"              formControlName=\"{column.Name.FirstCharToLowerCase()}\"\r\n";
                  c += $"              name=\"{column.Name.FirstCharToLowerCase()}\"\r\n";
                  c += $"              tableName=\"{dataItem}\"\r\n";
                  c += $"              tableLabelFieldName=\"name\"\r\n";
                  c += $"              [(ngModel)]=\"model.{column.Name.FirstCharToLowerCase()}\"\r\n";
                  c += $"              id=\"{column.Name.FirstCharToLowerCase()}\"\r\n";
                  c += $"              placeholder=\"Selecione...\">\r\n";
                  c += $"            </custom-select>\r\n";
               }
               else
               {
                  c += $"            <mat-label class=\"font-semibold\">{column.Name}</mat-label>\r\n";
                  c += $"            <input matInput type=\"number\"  name=\"{column.Name.FirstCharToLowerCase()}\" {required} [(ngModel)]=\"model.{column.Name.FirstCharToLowerCase()}\" formControlName=\"{column.Name.FirstCharToLowerCase()}\" />\r\n";
               }
            }
            //   c += "          </div>\r\n";
            c += "        </mat-form-field>\r\n";

            if (!column.IsPrimaryKey)
            {
               data.Add(c);
            }
         }

         return data;
      }

      public static List<string> GetEntityProperties(TableToGenerate tb)
      {
         List<string> data = new List<string>();

         foreach (var column in tb.ColumList)
         {
            string c = "        public ";
            c += GetEntityColumnType(column) + (column.IsNullable ? "?" : "");
            c += " " + column.Name + " { get; set; }\r\n";
            data.Add(c);
         }

         // Adicionar propriedades de navegação para FKs, evitando duplicatas
         HashSet<string> addedNavProps = new HashSet<string>();
         foreach (var relation in tb.tableRelations)
         {
            if (!addedNavProps.Contains(relation.PrimaryTableName))
            {
               string navProp = $"        public virtual {relation.PrimaryTableName} {relation.PrimaryTableName} {{ get; set; }}\r\n";
               data.Add(navProp);
               addedNavProps.Add(relation.PrimaryTableName);
            }
         }

         return data;
      }

      public static List<string> GetEntityBasicFilters(TableToGenerate tb)
      {
         List<string> data = new List<string>();

         foreach (var column in tb.ColumList)
         {
            string c = "";
            var cType = GetEntityColumnType(column).Replace("?", "");
            if (cType == "string")
            {
               c = $"            if (!string.IsNullOrEmpty(filters.{column.Name}))\r\n";
               c += $"                queryFilter = queryFilter.Where(_ => _.{column.Name}.Contains(filters.{column.Name}));\r\n\r\n";

               data.Add(c);
            }
            else if (cType == "DateTime")
            {
               c = $"            if (filters.{column.Name} != null)\r\n";
               c += "            {\r\n";
               c += $"                DateTime Start = new DateTime(Convert.ToDateTime(filters.{column.Name}).Year, Convert.ToDateTime(filters.{column.Name}).Month, Convert.ToDateTime(filters.{column.Name}).Day);\r\n";
               c += $"                DateTime End = Start.AddDays(1);\r\n";
               c += $"                queryFilter = queryFilter.Where(_ => _.{column.Name} >= Start && _.{column.Name} < End);\r\n";
               c += "            }\r\n\r\n";

            }
            else if (cType == "int" || cType == "long")
            {
               c = $"            if (filters.{column.Name} != null && filters.{column.Name} != 0)\r\n";
               c += $"                queryFilter = queryFilter.Where(_ => _.{column.Name} == filters.{column.Name});\r\n";
            }
            else if (cType == "bool")
            {
               c = $"            if (filters.{column.Name} != null)\r\n";
               c += $"                queryFilter = queryFilter.Where(_ => _.{column.Name} == filters.{column.Name});\r\n";
            }
            data.Add(c);
         }

         return data;
      }

      public static string GetEntityColumnType(TableColum dc)
      {
         string stype = "";
         string type = dc.Type.ToLower();
         
         // Tipos numéricos decimais (SQL Server e MySQL)
         if (type == "decimal" || type == "numeric") { stype = "decimal"; }
         
         // Tipos booleanos (SQL Server e MySQL)
         else if (type == "bit" || type == "boolean" || type == "bool" || type == "tinyint") { stype = "bool"; }
         
         // Tipos de string (SQL Server e MySQL)
         else if (type == "char" || type == "varchar" || type == "nvarchar" || type == "text" || 
                  type == "longtext" || type == "mediumtext" || type == "tinytext" || 
                  type == "uniqueidentifier" || type == "json") { stype = "string"; }
         
         // Tipos de data/hora (SQL Server e MySQL)
         else if (type == "datetime" || type == "date" || type == "timestamp" || 
                  type == "datetime2" || type == "smalldatetime" || type == "time") { stype = "DateTime"; }
         
         // Tipos de ponto flutuante (SQL Server e MySQL)
         else if (type == "float" || type == "double" || type == "real" || type == "money" || type == "smallmoney") { stype = "double"; }
         
         // Tipos inteiros (SQL Server e MySQL)
         else if (type == "int" || type == "integer" || type == "smallint" || type == "mediumint") { stype = "int"; }
         
         // Tipos inteiros grandes (SQL Server e MySQL)
         else if (type == "bigint") { stype = "long"; }
         
         // Tipos binários (tratados como byte array, mas usando string por simplicidade)
         else if (type == "binary" || type == "varbinary" || type == "blob" || type == "longblob" || 
                  type == "mediumblob" || type == "tinyblob") { stype = "byte[]"; }
         
         else stype = "string"; // Default para string ao invés de "unknown"

         return stype;
      }

      public static string FirstCharToLowerCase(this string? str)
      {
         if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]))
            return str.Length == 1 ? char.ToLower(str[0]).ToString() : char.ToLower(str[0]) + str[1..];

         return str ?? "";
      }

      public static List<string> GetEntityTableColumnsConfig(TableToGenerate tb)
      {
         List<string> data = new List<string>();
         foreach (var column in tb.ColumList)
         {
            if (!column.IsPrimaryKey)
            {
               string columnType = "text";
               if (column.Type.ToLower() == "int" || column.Type.ToLower() == "bigint" || column.Type.ToLower() == "smallint" || column.Type.ToLower() == "decimal" || column.Type.ToLower() == "numeric" || column.Type.ToLower() == "float" || column.Type.ToLower() == "double")
               {
                  columnType = "numeric";
               }
               else if (column.Type.ToLower() == "bit" || column.Type.ToLower() == "boolean")
               {
                  columnType = "boolean";
               }
               else if (column.Type.ToLower() == "date" || column.Type.ToLower() == "datetime" || column.Type.ToLower() == "datetime2" || column.Type.ToLower() == "timestamp")
               {
                  columnType = "date";
               }

               string c = $"      {{\n";
               c += $"        field: '{column.Name.FirstCharToLowerCase()}',\n";
               c += $"        header: '{column.Name}',\n";
               c += $"        type: '{columnType}',\n";
               c += $"        sortable: true,\n";
               c += $"        filterable: true\n";
               c += $"      }},\n";
               data.Add(c);
            }
         }
         return data;
      }

      public static List<string> GetEntityConfiguratioRelations(TableToGenerate tb)
      {
         List<string> data = new List<string>();

         foreach (var relation in tb.tableRelations)
         {
            string c = $"            builder.HasOne(p => p.{relation.PrimaryTableName})\r\n";
            c += $"                .WithMany()\r\n";
            c += $"                .HasForeignKey(p => p.{relation.ReferenceColumnName})\r\n";
            c += $"                .OnDelete(DeleteBehavior.Restrict);\r\n\r\n";
            data.Add(c);
         }

         return data;
      }

      public static List<string> GetEntityConfiguratioProperties(TableToGenerate tb)
      {
         List<string> data = new List<string>();

         foreach (var column in tb.ColumList)
         {
            string c = "";
            if (!column.IsPrimaryKey)
            {
               // Configurar propriedades básicas
               if (!column.IsNullable)
               {
                  c += $"            builder.Property(p => p.{column.Name}).IsRequired();\r\n";
               }

               // Configurar tamanho máximo para strings
               if ((column.Type.ToLower() == "char" || column.Type.ToLower() == "varchar" || column.Type.ToLower() == "nvarchar") && column.Length > 0)
               {
                  c += $"            builder.Property(p => p.{column.Name}).HasMaxLength({column.Length});\r\n";
               }

               if (!string.IsNullOrEmpty(c))
               {
                  data.Add(c);
               }
            }
         }

         return data;
      }

      public static List<string> GetEntityHtmlFormFields(TableToGenerate tb)
      {
         // Para register, usar Bootstrap form fields minimalistas
         return GetEntityHtmlBootstrapFormFields(tb);
      }

      public static List<string> GetEntityModelProperties(TableToGenerate tb)
      {
         List<string> data = new List<string>();

         foreach (var column in tb.ColumList)
         {
            string c = "  ";
            c += column.Name.FirstCharToLowerCase() + ": ";
            c += GetTypeScriptType(column) + (column.IsNullable ? " | null" : "") + ";\n";
            data.Add(c);
         }

         return data;
      }

      public static string GetTypeScriptType(TableColum dc)
      {
         string stype = "";
         string type = dc.Type.ToLower();

         // Tipos numéricos decimais
         if (type == "decimal" || type == "numeric") { stype = "number"; }

         // Tipos booleanos
         else if (type == "bit" || type == "boolean" || type == "bool" || type == "tinyint") { stype = "boolean"; }

         // Tipos de string
         else if (type == "char" || type == "varchar" || type == "nvarchar" || type == "text" ||
                  type == "longtext" || type == "mediumtext" || type == "tinytext" ||
                  type == "uniqueidentifier" || type == "json") { stype = "string"; }

         // Tipos de data/hora
         else if (type == "datetime" || type == "date" || type == "timestamp" ||
                  type == "datetime2" || type == "smalldatetime" || type == "time") { stype = "Date"; }

         // Tipos de ponto flutuante
         else if (type == "float" || type == "double" || type == "real" || type == "money" || type == "smallmoney") { stype = "number"; }

         // Tipos inteiros
         else if (type == "int" || type == "integer" || type == "smallint" || type == "mediumint") { stype = "number"; }

         // Tipos inteiros grandes
         else if (type == "bigint") { stype = "number"; }

         // Tipos binários (tratados como string base64)
         else if (type == "binary" || type == "varbinary" || type == "blob" || type == "longblob" ||
                  type == "mediumblob" || type == "tinyblob") { stype = "string"; }

         else stype = "string"; // Default para string

         return stype;
      }
   }
}
