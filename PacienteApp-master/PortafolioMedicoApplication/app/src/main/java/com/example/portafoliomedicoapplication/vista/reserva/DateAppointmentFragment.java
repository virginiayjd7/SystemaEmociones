package com.example.portafoliomedicoapplication.vista.reserva;

import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.example.portafoliomedicoapplication.R;
import com.example.portafoliomedicoapplication.interfaces.ReservaInterface;
import com.example.portafoliomedicoapplication.modelo.AtencionModelo;
import com.example.portafoliomedicoapplication.modelo.UsuarioModelo;
import com.example.portafoliomedicoapplication.presentador.atencion.AtencionAdaptador;
import com.example.portafoliomedicoapplication.interfaces.AtencionInterface;

import java.util.ArrayList;
import java.util.List;

/**
 * A simple {@link Fragment} subclass.
 */
public class DateAppointmentFragment extends Fragment implements AtencionInterface.RowListener {
    private UsuarioModelo Ojex;

    private List<AtencionModelo> listaAtencionModelo;
    ReservaInterface.VistaDialogReserva inter;
   // public DateAppointmentFragment(ReservaInterface.VistaDialogReserva inter) {
       // this.inter = inter;
   // }
    public  DateAppointmentFragment(UsuarioModelo objx)
    {
        Ojex=objx;
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        listaAtencionModelo = new ArrayList<>();
        listaAtencionModelo.add(new AtencionModelo());//rango de hora ejemplo 10:00am 12:00pm
        listaAtencionModelo.add(new AtencionModelo());
        listaAtencionModelo.add(new AtencionModelo());
        listaAtencionModelo.add(new AtencionModelo());
        listaAtencionModelo.add(new AtencionModelo());
        listaAtencionModelo.add(new AtencionModelo());
        listaAtencionModelo.add(new AtencionModelo());
        listaAtencionModelo.add(new AtencionModelo());

        View view = inflater.inflate(R.layout.fragment_date_appointment, container, false);
        RecyclerView recycler = view.findViewById(R.id.recycler_hour_range_appointment);

        TextView precio = view.findViewById(R.id.price_appointment);
        TextView nameespe = view.findViewById(R.id.nombre_doctor);
        nameespe.setText(Ojex.getNombres());
        precio.setText("S/. " + String.valueOf(Ojex.getPrecioConsulta().toString()));

        AtencionAdaptador atencionAdaptador = new AtencionAdaptador(listaAtencionModelo,R.layout.component_row_hour_range_appointment,this);
        LinearLayoutManager llms = new LinearLayoutManager(getContext());
        llms.setOrientation(LinearLayoutManager.VERTICAL);
        recycler.setLayoutManager(llms);
        recycler.setAdapter(atencionAdaptador);
        return view;
    }

    @Override
    public void onItemClickReserveAppointment() {
        Toast.makeText(getContext(), "Su reserva fue exitosa", Toast.LENGTH_SHORT).show();
        inter.nextItemPage();
    }

//    @Override
//    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
//        super.onViewCreated(view, savedInstanceState);
//    }
}
