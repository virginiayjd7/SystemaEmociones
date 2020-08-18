package com.example.portafoliomedicoapplication.presentador.especialidad;

import com.example.portafoliomedicoapplication.interfaces.EspecialidadInterface;
import com.example.portafoliomedicoapplication.modelo.EspecialidadModelo;

import java.util.List;

public class EspecilidadPresentador implements EspecialidadInterface.Presentador {
    EspecialidadModelo modelo;
    EspecialidadInterface.VistaList vistalist;

    public EspecilidadPresentador(EspecialidadInterface.VistaList vistalist) {
        modelo = new EspecialidadModelo(this);
        this.vistalist = vistalist;
    }

    @Override
    public void ejecutarListarEspecialidad() {
        modelo.listarEspecilidad();
    }

    @Override
    public void cuandoListaEspecilidadExitoso(List<EspecialidadModelo> list) {
        vistalist.manejadorListaEspecilidadExitoso(list);
    }

    @Override
    public void cuandoListaEspecilidadFallido() {
    }
}
