package com.example.portafoliomedicoapplication.presentador.usuario;

import com.example.portafoliomedicoapplication.interfaces.UsuarioInterface;
import com.example.portafoliomedicoapplication.modelo.UsuarioModelo;

import java.util.List;

public class UsuarioPresentador implements UsuarioInterface.Presentador {

    UsuarioModelo modelo;
    UsuarioInterface.VistaLoginActivity vistaLoginActivity;
    UsuarioInterface.VistaDashboardFragment vistaDashboardFragment;

    public UsuarioPresentador(UsuarioInterface.VistaLoginActivity vistaLoginActivity) {
        this.modelo = new UsuarioModelo(this);
        this.vistaLoginActivity = vistaLoginActivity;
    }

    public UsuarioPresentador(UsuarioInterface.VistaDashboardFragment vistaDashboardFragment) {
        this.modelo = new UsuarioModelo(this);
        this.vistaDashboardFragment = vistaDashboardFragment;
    }

    @Override
    public void ejecutarInicioSesion(String usuario , String clave) {
        modelo.iniciarSesion(usuario , clave);
    }

    @Override
    public void ejecutarListarDoctorPorEspecialidad(String idEspecialidad) {
        modelo.listarDoctorPorEspecialidad(idEspecialidad);
    }

    @Override
    public void cuandoInicioSesionExitoso(UsuarioModelo usuarioLogueado) {
        vistaLoginActivity.manejadorInicioSesionExitoso(usuarioLogueado);
    }

    @Override
    public void cuandoInicioSesionFallido() {
        vistaLoginActivity.manejadorInicioSesionFallido();
    }

    @Override
    public void cuandoListarDoctorPorEspecialidadExitoso(List<UsuarioModelo> listaUsuarioDoctor) {
        vistaDashboardFragment.manejadorListarDoctorPorEspecialidadExitoso(listaUsuarioDoctor);
    }

    @Override
    public void cuandoListarDoctorPorEspecialidadFallido() {
        vistaDashboardFragment.manejadorListarDoctorPorEspecialidadFallido("error");
    }
}
