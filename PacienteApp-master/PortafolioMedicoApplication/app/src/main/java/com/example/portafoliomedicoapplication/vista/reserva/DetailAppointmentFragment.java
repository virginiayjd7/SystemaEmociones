package com.example.portafoliomedicoapplication.vista.reserva;

import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.portafoliomedicoapplication.R;
import com.example.portafoliomedicoapplication.modelo.UsuarioModelo;

/**
 * A simple {@link Fragment} subclass.
 */
public class DetailAppointmentFragment extends Fragment {
    private UsuarioModelo Ojex;
    private int position;

    public DetailAppointmentFragment(UsuarioModelo objx) {
        Ojex=objx;
    }



    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        //return inflater.inflate(R.layout.fragment_detail_appointment, container, false);
        View view = inflater.inflate(R.layout.fragment_detail_appointment, container, false);
        ImageView photoDocor = view.findViewById(R.id.doctor_photo_appointment);
        //        ##################
        TextView nameespe = view.findViewById(R.id.textView2);
        //
        TextView nombreent = view.findViewById(R.id.textView3);
        TextView precio = view.findViewById(R.id.textView5);
        TextView telefono= view.findViewById(R.id.textView7);
        TextView direccion= view.findViewById(R.id.textView9);
        TextView correo= view.findViewById(R.id.textView10);
        //
        TextView fechainicio= view.findViewById(R.id.textView16);
        TextView fechafinal = view.findViewById(R.id.textView18);
        TextView costo = view.findViewById(R.id.textView21);
        //...........................
        nameespe.setText(Ojex.getNombres());
        //
        nombreent.setText(Ojex.getEspecialidadNombre());
        precio.setText("S/. " + String.valueOf(Ojex.getPrecioConsulta().toString()));
        telefono.setText(Ojex.getTelefono());
        direccion.setText(Ojex.getDireccion());
        correo.setText(Ojex.getEmail());
       //
        fechainicio.setText(Ojex.getFecha());
        fechafinal.setText(Ojex.getFecha());
        costo.setText("S/. " + String.valueOf(Ojex.getPrecioConsulta().toString()));
       //

        return view;
    }
}
