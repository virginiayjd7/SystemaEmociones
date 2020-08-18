package com.example.portafoliomedicoapplication.presentador.doctor;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.example.portafoliomedicoapplication.interfaces.DoctorInterface;
import com.example.portafoliomedicoapplication.interfaces.UsuarioInterface;
import com.example.portafoliomedicoapplication.modelo.UsuarioModelo;

import java.util.List;

public class DoctorAdapter  extends RecyclerView.Adapter<DoctorHolder> {

    private int resourceLayout;
    private List<UsuarioModelo> listaDoctor;
    private UsuarioInterface.RowListener rowListener;
    public DoctorAdapter(int resourceLayout , List<UsuarioModelo> listaDoctor, UsuarioInterface.RowListener rowListener) {
        this.resourceLayout = resourceLayout;
        this.listaDoctor = listaDoctor;
        this.rowListener = rowListener;
    }

    @NonNull
    @Override
    public DoctorHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(resourceLayout,parent,false);
        return new DoctorHolder(view,rowListener);
    }

    @Override
    public void onBindViewHolder(@NonNull DoctorHolder holder, int position) {
        UsuarioModelo usuarioDoctor = listaDoctor.get(position);
//        holder.imgProfile.setImageURI();
        holder.txvFullName.setText(usuarioDoctor.getNombres());
        holder.txvEspecialidad.setText(usuarioDoctor.getEspecialidadNombre());
        holder.txvPrice.setText("S/. "+String.valueOf(usuarioDoctor.getPrecioConsulta()));
        Glide.with(holder.cardView.getContext())
                .load(usuarioDoctor.getAvatar())
                .circleCrop()
                .into(holder.imgProfile);
        holder.bindListenerClickRow(holder.cardView,usuarioDoctor);/////////////////////////xxxxxxxxxxxxxxxx

    }

    @Override
    public int getItemCount() {
        return listaDoctor.size();
    }


}
