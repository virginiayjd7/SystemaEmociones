package com.example.portafoliomedicoapplication.vista.reserva;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentPagerAdapter;

import com.example.portafoliomedicoapplication.interfaces.ReservaInterface;
import com.example.portafoliomedicoapplication.modelo.UsuarioModelo;


public class SliderPagerAppointmentAdapter extends FragmentPagerAdapter {

    private UsuarioModelo usuarioDoctor;
    ReservaInterface.VistaDialogReserva inter;
    public SliderPagerAppointmentAdapter(@NonNull FragmentManager fm, int behavior, ReservaInterface.VistaDialogReserva inter) {
        super(fm, behavior);
        this.inter = inter;
    }

    @NonNull
    @Override
    public Fragment getItem(int position) {

        Fragment f = new InfoDoctorAppointmentFragment(this.obtenerParametroUsuarioDoctor());////XX
        if(position == 0){
            f = new InfoDoctorAppointmentFragment(this.obtenerParametroUsuarioDoctor());////////XX
        }
        if(position == 1){
            f = new DateAppointmentFragment(this.obtenerParametroUsuarioDoctor());
        }
        if(position == 2){
            f = new DetailAppointmentFragment(this.obtenerParametroUsuarioDoctor());

        }
        return f;
    }

    //SETTER/////XXXX
    public void pasarParametroUsuarioDoctor(UsuarioModelo objUsuarioDoctor){
        this.usuarioDoctor = objUsuarioDoctor;
    }

    //GETTER/XXXX
    public UsuarioModelo obtenerParametroUsuarioDoctor(){
        return this.usuarioDoctor;
    }

    @Override
    public int getCount() {
        return 3;
    }
}
