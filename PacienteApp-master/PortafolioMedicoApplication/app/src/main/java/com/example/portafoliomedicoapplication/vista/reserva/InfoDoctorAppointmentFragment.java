package com.example.portafoliomedicoapplication.vista.reserva;

import android.media.Image;
import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.bumptech.glide.Glide;
import com.example.portafoliomedicoapplication.R;
import com.example.portafoliomedicoapplication.modelo.UsuarioModelo;

/**
 * A simple {@link Fragment} subclass.
 */
public class InfoDoctorAppointmentFragment extends Fragment {

    private UsuarioModelo Ojex;
    public InfoDoctorAppointmentFragment() {
        // Required empty public constructor
    }

    public InfoDoctorAppointmentFragment(UsuarioModelo objex) {
        // Required empty public constructor
        Ojex=objex;///////save
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view = inflater.inflate(R.layout.fragment_info_doctor_appointment, container, false);
        ImageView photoDocor = view.findViewById(R.id.doctor_photo_appointment);
        //        ##################

        TextView namedoctor = view.findViewById(R.id.doctor_name);
        TextView especialidad=view.findViewById(R.id.especi);
        TextView telefono=view.findViewById(R.id.tele);
        TextView precio=view.findViewById(R.id.buy);


        namedoctor.setText(Ojex.getNombres());
        especialidad.setText(Ojex.getEspecialidadNombre());
        telefono.setText(Ojex.getTelefono());
        precio.setText("S/. "+String.valueOf (Ojex.getPrecioConsulta().toString()));

//        view.findViewById(R.id.doctor_name);

        Glide.with(this)
                .load(Ojex.getAvatar())
                .circleCrop()
                .into(photoDocor);

                //PERFIL
                //RESERVAS
        return view;

    }
}
