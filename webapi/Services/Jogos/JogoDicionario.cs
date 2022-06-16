using ProjetoIntegrador.Api.Extensions;
using ProjetoIntegrador.Api.Models;
using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Services
{
    public class JogoDicionario
    {
        private Dictionary<TiposJogo, JogoRecord> _dictEnum;
        private Dictionary<string, JogoRecord> _dictDescription;
        private Dictionary<int, JogoRecord> _dictValue;

        public JogoDicionario()
        {
            _dictDescription = new Dictionary<string, JogoRecord>();
            _dictEnum = new Dictionary<TiposJogo, JogoRecord>();
            _dictValue = new Dictionary<int, JogoRecord>();

            foreach (TiposJogo item in Enum.GetValues(typeof(TiposJogo)))
            {
                Add(new JogoRecord(item));
            }
        }

        private void Add(JogoRecord record)
        {
            _dictEnum.Add(record.Tipo, record);
            _dictValue.Add(record.Valor, record);
            _dictDescription.Add(record.Descricao, record);
        }

        public TiposJogo? this[int index]
        {
            get 
            { 
                if(_dictValue.Exists(index)) 
                    return _dictValue[index].Tipo;
                return null;
            }
        }

        public TiposJogo? this[string index]
        {
            get 
            {
                if (_dictDescription.Exists(index))
                    return _dictDescription[index].Tipo;
                return null;
            }
        }

        public string this[TiposJogo index]
        {
            get 
            { 
                return _dictEnum[index].Descricao; 
            }
        }
    }
}