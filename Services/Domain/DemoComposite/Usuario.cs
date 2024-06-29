///////////////////////////////////////////////////////////
//  User.cs
//  Implementation of the Class User
//  Generated by Enterprise Architect
//  Created on:      02-may.-2024 21:18:24
//  Original author: gaston
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;



public class Usuario {

    public Guid IdUsuario { get; set; }
    public string UserName { get; set; }

    public string Password { get; set; }


    public List<Acceso> Accesos = new List<Acceso>();

	public Usuario(){
        IdUsuario = Guid.NewGuid();
	}

    public Usuario(Guid idUsuario)
    {
        this.IdUsuario = idUsuario;
    }

    public List<Patente> GetPatentes()
    {
        List<Patente> patentes= new List<Patente>();

        GetAllPatentes(Accesos, patentes);

        return patentes;
    }

    private void GetAllPatentes(List<Acceso> accesos, List<Patente> patentesReturn)
    {
        foreach(var acceso in accesos)
        {
            //Cu�l ser�a mi condici�n de corte?
            //Significa que estoy ante un elemento de tipo Leaf, Hoja, Primitivo
            if (acceso.GetCount() == 0)
            {
                //Podr�a pasar que la patente ya est� agregada (Similar a un distinct)
                if(!patentesReturn.Any(o => o.Id == acceso.Id))
                    patentesReturn.Add(acceso as Patente);
            }
            else
            {
                //Tengo que tratar a mi "acceso" como si fuese una familia
                GetAllPatentes((acceso as Familia).Accesos, patentesReturn);
            }
        }
    }

    public List<Familia> GetFamilias() {

        List<Familia> familias= new List<Familia>();

        GetAllFamilias(Accesos, familias);

        return familias;

    }

    private void GetAllFamilias(List<Acceso> accesos, List<Familia> famililaReturn)
    {
        foreach (var acceso in accesos)
        {
            //Cu�l ser�a mi condici�n de corte?
            //Significa que estoy ante un elemento de tipo Composite
            if (acceso.GetCount() > 0)
            {
                if (!famililaReturn.Any(o => o.Id == acceso.Id))
                    famililaReturn.Add(acceso as Familia);

                GetAllFamilias((acceso as Familia).Accesos, famililaReturn);
            }
        }
    }

}//end User