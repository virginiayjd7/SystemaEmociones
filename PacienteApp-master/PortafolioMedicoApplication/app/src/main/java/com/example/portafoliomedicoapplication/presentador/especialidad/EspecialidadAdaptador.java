package com.example.portafoliomedicoapplication.presentador.especialidad;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.portafoliomedicoapplication.interfaces.EspecialidadInterface;
import com.example.portafoliomedicoapplication.modelo.EspecialidadModelo;


import java.util.List;

public class EspecialidadAdaptador extends RecyclerView.Adapter<EspecialidadHolder> {
    private int resourceLayout;
    private List<EspecialidadModelo> listaEspecialidadModelo;
    EspecialidadInterface.RowListener rowListener;

    public EspecialidadAdaptador(int resourceLayout, List<EspecialidadModelo> listaEspecialidadModelo , EspecialidadInterface.RowListener rowListener) {
        this.resourceLayout = resourceLayout;
        this.listaEspecialidadModelo = listaEspecialidadModelo;
        this.rowListener = rowListener;
    }

    @NonNull
    @Override
    public EspecialidadHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(resourceLayout,parent,false);
        return new EspecialidadHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull EspecialidadHolder holder, int position) {
        final EspecialidadModelo objEspecialidad = listaEspecialidadModelo.get(position);
        holder.txvName.setText(objEspecialidad.getNombre());
        holder.elementoEspecialidad.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                rowListener.cuandoClickElementoEspecialidad(objEspecialidad.getIdEspecialidad());
            }
        });
    }

    @Override
    public int getItemCount() {
        return listaEspecialidadModelo.size();
    }
}
