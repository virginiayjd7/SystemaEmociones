package com.example.portafoliomedicoapplication.presentador.atencion;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.portafoliomedicoapplication.interfaces.AtencionInterface;
import com.example.portafoliomedicoapplication.modelo.AtencionModelo;


import java.util.List;

public class AtencionAdaptador extends RecyclerView.Adapter<AtencionHolder> {

    private List<AtencionModelo> listaAtencionModelo;
    private int resourceLayout;
    private AtencionInterface.RowListener rowListener;

    public AtencionAdaptador(List<AtencionModelo> listaAtencionModelo, int resourceLayout, AtencionInterface.RowListener rowListener) {
        this.listaAtencionModelo = listaAtencionModelo;
        this.resourceLayout = resourceLayout;
        this.rowListener = rowListener;
    }

    @NonNull
    @Override
    public AtencionHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(resourceLayout,parent,false);
        return new AtencionHolder(view ,rowListener);
    }

    @Override
    public void onBindViewHolder(@NonNull AtencionHolder holder, int position) {
        holder.bindOnClickButtonReserveAppointment(holder.btnReserveAppointment);
    }

    @Override
    public int getItemCount() {
        return listaAtencionModelo.size();
    }
}
