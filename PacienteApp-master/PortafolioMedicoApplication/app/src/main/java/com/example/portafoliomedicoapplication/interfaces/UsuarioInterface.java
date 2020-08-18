package com.example.portafoliomedicoapplication.interfaces;


import com.example.portafoliomedicoapplication.modelo.UsuarioModelo;

import java.util.List;

public interface UsuarioInterface {

    interface VistaLoginActivity{
        void manejadorIniciarSesion();
        void manejadorInicioSesionExitoso(UsuarioModelo usuarioLogueado);
        void manejadorInicioSesionFallido();
    }

    interface VistaDashboardFragment{//listar doctor por especialidad
        void manejadorListarDoctorPorEspecialidad(String idEspecialidad);
        void manejadorListarDoctorPorEspecialidadExitoso(List<UsuarioModelo> listaUsuarioDoctor);//////////////////xxxxxxxxxxxx
        void manejadorListarDoctorPorEspecialidadFallido(String error);
    }

    interface Presentador{
        void ejecutarInicioSesion(String usuario , String clave);
        void ejecutarListarDoctorPorEspecialidad(String idEspecialidad);

        //Callbacks
        void cuandoInicioSesionExitoso(UsuarioModelo usuarioLogueado);
        void cuandoInicioSesionFallido();

        void cuandoListarDoctorPorEspecialidadExitoso(List<UsuarioModelo> listaUsuarioDoctor);
        void cuandoListarDoctorPorEspecialidadFallido();
    }

    interface Modelo{
        void iniciarSesion(String usuario , String clave);//
        void listarDoctorPorEspecialidad(String idEspecialidad);
    }

    interface RowListener {
        void onClickDoctorRow(UsuarioModelo objx);//clic en la fila
    }
}