package com.example.portafoliomedicoapplication.interfaces;

import com.example.portafoliomedicoapplication.modelo.EspecialidadModelo;

import java.util.List;

public interface EspecialidadInterface {

    interface VistaList{
        void menejadorListarEspecialidad();

        void manejadorListaEspecilidadExitoso(List<EspecialidadModelo> list);
    }

    interface Presentador{
        void ejecutarListarEspecialidad();


        void cuandoListaEspecilidadExitoso(List<EspecialidadModelo> list);
        void cuandoListaEspecilidadFallido();
    }

    interface Modelo{
        void listarEspecilidad();
        void agregarEspecialidad(EspecialidadModelo objEspecialidad);
    }

    interface RowListener{
        void cuandoClickElementoEspecialidad(String idEspecialidad);
    }

}
