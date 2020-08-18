package com.example.portafoliomedicoapplication.modelo;

import android.util.Log;

import androidx.annotation.NonNull;

import com.example.portafoliomedicoapplication.interfaces.EspecialidadInterface;
import com.example.portafoliomedicoapplication.presentador.especialidad.EspecilidadPresentador;
import com.github.javafaker.Faker;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.firestore.DocumentReference;
import com.google.firebase.firestore.DocumentSnapshot;
import com.google.firebase.firestore.QuerySnapshot;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

public class EspecialidadModelo implements EspecialidadInterface.Modelo , Serializable {
    private static final String TAG = EspecialidadModelo.class.getSimpleName();
    private String idEspecialidad;
    private String nombre;
    private String descripcion;
    private String estado;
    private String urlMiniatura;

    EspecilidadPresentador especilidadPresentador;

    public EspecialidadModelo(EspecilidadPresentador especilidadPresentador) {
        this.especilidadPresentador = especilidadPresentador;
    }

    public EspecialidadModelo() {
    }


    public String getIdEspecialidad() {
        return idEspecialidad;
    }

    public void setIdEspecialidad(String idEspecialidad) {
        this.idEspecialidad = idEspecialidad;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }

    public String getEstado() {
        return estado;
    }

    public void setEstado(String estado) {
        this.estado = estado;
    }

    public String getUrlMiniatura() {
        return urlMiniatura;
    }

    public void setUrlMiniatura(String urlMiniatura) {
        this.urlMiniatura = urlMiniatura;
    }

    @Override
    public void listarEspecilidad() {
        Conexion.getCollectionEspecilidad().get().addOnCompleteListener(new OnCompleteListener<QuerySnapshot>() {
            @Override
            public void onComplete(@NonNull Task<QuerySnapshot> task) {
                //task.isSuccessful();
                final List<EspecialidadModelo> especialidadList = new ArrayList<>();
                for (DocumentSnapshot document : task.getResult().getDocuments()){
                    EspecialidadModelo objEspecialidad = document.toObject(EspecialidadModelo.class);
                    especialidadList.add(objEspecialidad);
                }
                Log.d("--ESP--" , especialidadList.size() + "<<<<");
                especilidadPresentador.cuandoListaEspecilidadExitoso(especialidadList);

                //este metodo fue tempora para crear unos cuantos doctores asignandoles especialidad
                // seederUsuarioDoctor(especialidadList);
            }
        });

    }

    @Override
    public void agregarEspecialidad(final EspecialidadModelo objEspecialidad) {
        final DocumentReference nuevoDocumentoEspecialidad = Conexion.getCollectionEspecilidad().document();
        objEspecialidad.setIdEspecialidad(nuevoDocumentoEspecialidad.getId());
        nuevoDocumentoEspecialidad.set(objEspecialidad).addOnCompleteListener(new OnCompleteListener<Void>() {
            @Override
            public void onComplete(@NonNull Task<Void> task) {
                if(task.isSuccessful()){
                    Log.d(TAG,("Nueva especialidad ID: " + objEspecialidad.getIdEspecialidad()));
                }
            }
        });
    }


    public void seederUsuarioDoctor(List<EspecialidadModelo> listaEspecialidad){
        Faker faker = new Faker(new Locale("es"));
        UsuarioModelo usuarioModelo = new UsuarioModelo();
        for(EspecialidadModelo objEspecialidad : listaEspecialidad){
            for(int i = 0 ; i < 10 ; i++){
                UsuarioModelo fakerDoctor = new UsuarioModelo();
                fakerDoctor.setNombres(faker.name().fullName().toLowerCase());
                fakerDoctor.setAvatar(faker.avatar().image());
                fakerDoctor.setBiografia(faker.lorem().paragraph());
                fakerDoctor.setCelular(faker.phoneNumber().cellPhone());
                fakerDoctor.setTelefono(faker.phoneNumber().phoneNumber());
                fakerDoctor.setDistrito("TACNA");
                fakerDoctor.setPais("PERÚ");
                fakerDoctor.setDireccion(faker.address().streetName());
                fakerDoctor.setEmail(faker.internet().emailAddress());
                fakerDoctor.setFecha(faker.date().birthday().toString());
                fakerDoctor.setFechaNacimiento(faker.date().birthday().toString());
                fakerDoctor.setEstado("ACTIVO");
                fakerDoctor.setNroDocumento(faker.number().digits(8));
                fakerDoctor.setCiudad("TACNA");
                fakerDoctor.setPeso(faker.number().randomDouble(2,50 ,80));
                fakerDoctor.setTalla(faker.number().randomDouble(2, 160,180));
                fakerDoctor.setUsuario(fakerDoctor.getNombres().split(" ")[0]);
                fakerDoctor.setClave("123456");
                fakerDoctor.setTipoDocumento("DNI");
                fakerDoctor.setTipoUsuario(UsuarioModelo.TIPO_USUARIO_MEDICO);
                fakerDoctor.setIdEspecialidad(objEspecialidad.getIdEspecialidad());
                fakerDoctor.setEspecialidadNombre(objEspecialidad.getNombre());
                fakerDoctor.setPrecioConsulta(faker.number().randomDouble(0,2,2));
                fakerDoctor.setLatitud("-18.0175878");
                fakerDoctor.setLongitud("-70.25186");
                usuarioModelo.agregarDoctor(fakerDoctor);
            }
        }
    }
}
