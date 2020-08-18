package com.example.portafoliomedicoapplication;

import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.example.portafoliomedicoapplication.interfaces.EspecialidadInterface;
import com.example.portafoliomedicoapplication.interfaces.UsuarioInterface;
import com.example.portafoliomedicoapplication.modelo.EspecialidadModelo;
import com.example.portafoliomedicoapplication.modelo.UsuarioModelo;
import com.example.portafoliomedicoapplication.presentador.especialidad.EspecilidadPresentador;
import com.example.portafoliomedicoapplication.presentador.usuario.UsuarioPresentador;
import com.example.portafoliomedicoapplication.vista.reserva.ProcessAppointmentFragment;
import com.example.portafoliomedicoapplication.presentador.doctor.DoctorAdapter;
import com.example.portafoliomedicoapplication.interfaces.DoctorInterface;

import com.example.portafoliomedicoapplication.presentador.especialidad.EspecialidadAdaptador;

import java.util.ArrayList;
import java.util.List;


/**
 * A simple {@link Fragment} subclass.
 */
public class DashboardFragment extends Fragment implements UsuarioInterface.VistaDashboardFragment ,
        UsuarioInterface.RowListener,
        EspecialidadInterface.VistaList,
        EspecialidadInterface.RowListener{

    EspecilidadPresentador especilidadPresentador;
    UsuarioPresentador usuarioPresentador;
    RecyclerView recyclerSpecialty;
    RecyclerView recyclerDoctor;
    public DashboardFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        especilidadPresentador = new EspecilidadPresentador(this);
        usuarioPresentador = new UsuarioPresentador(this);
        // Inflate the layout for this fragment
        View view  = inflater.inflate(R.layout.fragment_dashboard, container, false);
        recyclerSpecialty = view.findViewById(R.id.recycler_specialty);
        recyclerDoctor = view.findViewById(R.id.recycler_doctor);
        menejadorListarEspecialidad();
        return view;
    }

//////
    @Override
    public void onClickDoctorRow(UsuarioModelo obejx) {
        ProcessAppointmentFragment processAppointmentFragment = new ProcessAppointmentFragment(obejx);
        processAppointmentFragment.show(getActivity().getSupportFragmentManager(), "your_dialog_fragment");

    }

    @Override
    public void menejadorListarEspecialidad() {
        especilidadPresentador.ejecutarListarEspecialidad();

    }

    @Override
    public void manejadorListaEspecilidadExitoso(List<EspecialidadModelo> listaEspecialidad) {

        EspecialidadAdaptador especialidadAdaptador = new EspecialidadAdaptador(R.layout.component_row_specialty,listaEspecialidad , this);
        LinearLayoutManager llms = new LinearLayoutManager(getContext());
        llms.setOrientation(LinearLayoutManager.HORIZONTAL);
        recyclerSpecialty.setLayoutManager(llms);
        recyclerSpecialty.setAdapter(especialidadAdaptador);

    }

    @Override
    public void manejadorListarDoctorPorEspecialidad(String idEspecialidad) {
        usuarioPresentador.ejecutarListarDoctorPorEspecialidad(idEspecialidad);
    }

    @Override
    public void manejadorListarDoctorPorEspecialidadExitoso(List<UsuarioModelo> listaUsuarioDoctor) {
        DoctorAdapter doctorAdapter = new DoctorAdapter(R.layout.component_row_doctor,listaUsuarioDoctor,this);
        LinearLayoutManager llm = new LinearLayoutManager(getContext());
        llm.setOrientation(LinearLayoutManager.VERTICAL);
        recyclerDoctor.setLayoutManager(llm);
        recyclerDoctor.setAdapter(doctorAdapter);////////////////////////////xxxxxxxxx
    }

    @Override
    public void manejadorListarDoctorPorEspecialidadFallido(String error) {

    }

    @Override
    public void cuandoClickElementoEspecialidad(String idEspecialidad) {
        manejadorListarDoctorPorEspecialidad(idEspecialidad);
    }
}
