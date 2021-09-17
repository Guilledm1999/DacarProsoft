﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class Ventas
    {
        public string Secuencia { get; set; }
        public string Cuenta { get; set; }
        public string Nombre_Cuenta { get; set; }
        public Nullable<int> Anio { get; set; }
        public string Mes { get; set; }
        public string Fecha { get; set; }
        public string Mercado { get; set; }
        public string Vendedor { get; set; }
        public string Cliente { get; set; }
        public string Clie_tipo { get; set; }
        public string Cliente_Linea { get; set; }
        public string Cliente_Clase { get; set; }
        public string prod_linea { get; set; }
        public string Prod_clase { get; set; }
        public string Prod_grupo { get; set; }
        public string Producto { get; set; }
        public string Marca { get; set; }
        public string ModeloBC { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> Cantidad { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public Nullable<decimal> Parcial { get; set; }
        public Nullable<decimal> porc_desc { get; set; }
        public Nullable<decimal> Descuento { get; set; }
        public Nullable<decimal> base0 { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public Nullable<decimal> impuesto { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> Costo_uni { get; set; }
        public Nullable<decimal> Costo_tot { get; set; }
        public Nullable<decimal> Peso_uni { get; set; }
        public Nullable<decimal> Peso_tot { get; set; }
        public string Tipo_de_Nc { get; set; }

    }
}