namespace Practice.Indications;

public record TotalKeratometry(CorneaValue TseMean, CorneaValue Tk1, CorneaValue Tk2, CorneaValue DeltaTkMean,
    List<CorneaValue> Sd, List<CorneaValue> Tse, List<CorneaValue> DeltaTk);