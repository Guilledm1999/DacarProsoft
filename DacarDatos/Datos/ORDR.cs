//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DacarDatos.Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class ORDR
    {
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string DocType { get; set; }
        public string CANCELED { get; set; }
        public string Handwrtten { get; set; }
        public string Printed { get; set; }
        public string DocStatus { get; set; }
        public string InvntSttus { get; set; }
        public string Transfered { get; set; }
        public string ObjType { get; set; }
        public Nullable<System.DateTime> DocDate { get; set; }
        public Nullable<System.DateTime> DocDueDate { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string Address { get; set; }
        public string NumAtCard { get; set; }
        public Nullable<decimal> VatPercent { get; set; }
        public Nullable<decimal> VatSum { get; set; }
        public Nullable<decimal> VatSumFC { get; set; }
        public Nullable<decimal> DiscPrcnt { get; set; }
        public Nullable<decimal> DiscSum { get; set; }
        public Nullable<decimal> DiscSumFC { get; set; }
        public string DocCur { get; set; }
        public Nullable<decimal> DocRate { get; set; }
        public Nullable<decimal> DocTotal { get; set; }
        public Nullable<decimal> DocTotalFC { get; set; }
        public Nullable<decimal> PaidToDate { get; set; }
        public Nullable<decimal> PaidFC { get; set; }
        public Nullable<decimal> GrosProfit { get; set; }
        public Nullable<decimal> GrosProfFC { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Comments { get; set; }
        public string JrnlMemo { get; set; }
        public Nullable<int> TransId { get; set; }
        public Nullable<int> ReceiptNum { get; set; }
        public Nullable<short> GroupNum { get; set; }
        public Nullable<short> DocTime { get; set; }
        public Nullable<int> SlpCode { get; set; }
        public Nullable<short> TrnspCode { get; set; }
        public string PartSupply { get; set; }
        public string Confirmed { get; set; }
        public Nullable<short> GrossBase { get; set; }
        public Nullable<int> ImportEnt { get; set; }
        public string CreateTran { get; set; }
        public string SummryType { get; set; }
        public string UpdInvnt { get; set; }
        public string UpdCardBal { get; set; }
        public short Instance { get; set; }
        public Nullable<int> Flags { get; set; }
        public string InvntDirec { get; set; }
        public Nullable<int> CntctCode { get; set; }
        public string ShowSCN { get; set; }
        public string FatherCard { get; set; }
        public Nullable<decimal> SysRate { get; set; }
        public string CurSource { get; set; }
        public Nullable<decimal> VatSumSy { get; set; }
        public Nullable<decimal> DiscSumSy { get; set; }
        public Nullable<decimal> DocTotalSy { get; set; }
        public Nullable<decimal> PaidSys { get; set; }
        public string FatherType { get; set; }
        public Nullable<decimal> GrosProfSy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string IsICT { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<decimal> Volume { get; set; }
        public Nullable<short> VolUnit { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<short> WeightUnit { get; set; }
        public Nullable<int> Series { get; set; }
        public Nullable<System.DateTime> TaxDate { get; set; }
        public string Filler { get; set; }
        public string DataSource { get; set; }
        public string StampNum { get; set; }
        public string isCrin { get; set; }
        public Nullable<int> FinncPriod { get; set; }
        public Nullable<short> UserSign { get; set; }
        public string selfInv { get; set; }
        public Nullable<decimal> VatPaid { get; set; }
        public Nullable<decimal> VatPaidFC { get; set; }
        public Nullable<decimal> VatPaidSys { get; set; }
        public Nullable<short> UserSign2 { get; set; }
        public string WddStatus { get; set; }
        public Nullable<int> draftKey { get; set; }
        public Nullable<decimal> TotalExpns { get; set; }
        public Nullable<decimal> TotalExpFC { get; set; }
        public Nullable<decimal> TotalExpSC { get; set; }
        public Nullable<int> DunnLevel { get; set; }
        public string Address2 { get; set; }
        public Nullable<int> LogInstanc { get; set; }
        public string Exported { get; set; }
        public Nullable<int> StationID { get; set; }
        public string Indicator { get; set; }
        public string NetProc { get; set; }
        public Nullable<decimal> AqcsTax { get; set; }
        public Nullable<decimal> AqcsTaxFC { get; set; }
        public Nullable<decimal> AqcsTaxSC { get; set; }
        public Nullable<decimal> CashDiscPr { get; set; }
        public Nullable<decimal> CashDiscnt { get; set; }
        public Nullable<decimal> CashDiscFC { get; set; }
        public Nullable<decimal> CashDiscSC { get; set; }
        public string ShipToCode { get; set; }
        public string LicTradNum { get; set; }
        public string PaymentRef { get; set; }
        public Nullable<decimal> WTSum { get; set; }
        public Nullable<decimal> WTSumFC { get; set; }
        public Nullable<decimal> WTSumSC { get; set; }
        public Nullable<decimal> RoundDif { get; set; }
        public Nullable<decimal> RoundDifFC { get; set; }
        public Nullable<decimal> RoundDifSy { get; set; }
        public string CheckDigit { get; set; }
        public Nullable<int> Form1099 { get; set; }
        public string Box1099 { get; set; }
        public string submitted { get; set; }
        public string PoPrss { get; set; }
        public string Rounding { get; set; }
        public string RevisionPo { get; set; }
        public short Segment { get; set; }
        public Nullable<System.DateTime> ReqDate { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public string PickStatus { get; set; }
        public string Pick { get; set; }
        public string BlockDunn { get; set; }
        public string PeyMethod { get; set; }
        public string PayBlock { get; set; }
        public Nullable<int> PayBlckRef { get; set; }
        public string MaxDscn { get; set; }
        public string Reserve { get; set; }
        public Nullable<decimal> Max1099 { get; set; }
        public string CntrlBnk { get; set; }
        public string PickRmrk { get; set; }
        public string ISRCodLine { get; set; }
        public Nullable<decimal> ExpAppl { get; set; }
        public Nullable<decimal> ExpApplFC { get; set; }
        public Nullable<decimal> ExpApplSC { get; set; }
        public string Project { get; set; }
        public string DeferrTax { get; set; }
        public string LetterNum { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<decimal> WTApplied { get; set; }
        public Nullable<decimal> WTAppliedF { get; set; }
        public string BoeReserev { get; set; }
        public string AgentCode { get; set; }
        public Nullable<decimal> WTAppliedS { get; set; }
        public Nullable<decimal> EquVatSum { get; set; }
        public Nullable<decimal> EquVatSumF { get; set; }
        public Nullable<decimal> EquVatSumS { get; set; }
        public Nullable<short> Installmnt { get; set; }
        public string VATFirst { get; set; }
        public Nullable<decimal> NnSbAmnt { get; set; }
        public Nullable<decimal> NnSbAmntSC { get; set; }
        public Nullable<decimal> NbSbAmntFC { get; set; }
        public Nullable<decimal> ExepAmnt { get; set; }
        public Nullable<decimal> ExepAmntSC { get; set; }
        public Nullable<decimal> ExepAmntFC { get; set; }
        public Nullable<System.DateTime> VatDate { get; set; }
        public string CorrExt { get; set; }
        public Nullable<int> CorrInv { get; set; }
        public Nullable<int> NCorrInv { get; set; }
        public string CEECFlag { get; set; }
        public Nullable<decimal> BaseAmnt { get; set; }
        public Nullable<decimal> BaseAmntSC { get; set; }
        public Nullable<decimal> BaseAmntFC { get; set; }
        public string CtlAccount { get; set; }
        public Nullable<int> BPLId { get; set; }
        public string BPLName { get; set; }
        public string VATRegNum { get; set; }
        public string TxInvRptNo { get; set; }
        public Nullable<System.DateTime> TxInvRptDt { get; set; }
        public string KVVATCode { get; set; }
        public string WTDetails { get; set; }
        public Nullable<int> SumAbsId { get; set; }
        public Nullable<System.DateTime> SumRptDate { get; set; }
        public string PIndicator { get; set; }
        public string ManualNum { get; set; }
        public string UseShpdGd { get; set; }
        public Nullable<decimal> BaseVtAt { get; set; }
        public Nullable<decimal> BaseVtAtSC { get; set; }
        public Nullable<decimal> BaseVtAtFC { get; set; }
        public Nullable<decimal> NnSbVAt { get; set; }
        public Nullable<decimal> NnSbVAtSC { get; set; }
        public Nullable<decimal> NbSbVAtFC { get; set; }
        public Nullable<decimal> ExptVAt { get; set; }
        public Nullable<decimal> ExptVAtSC { get; set; }
        public Nullable<decimal> ExptVAtFC { get; set; }
        public Nullable<decimal> LYPmtAt { get; set; }
        public Nullable<decimal> LYPmtAtSC { get; set; }
        public Nullable<decimal> LYPmtAtFC { get; set; }
        public Nullable<decimal> ExpAnSum { get; set; }
        public Nullable<decimal> ExpAnSys { get; set; }
        public Nullable<decimal> ExpAnFrgn { get; set; }
        public string DocSubType { get; set; }
        public string DpmStatus { get; set; }
        public Nullable<decimal> DpmAmnt { get; set; }
        public Nullable<decimal> DpmAmntSC { get; set; }
        public Nullable<decimal> DpmAmntFC { get; set; }
        public string DpmDrawn { get; set; }
        public Nullable<decimal> DpmPrcnt { get; set; }
        public Nullable<decimal> PaidSum { get; set; }
        public Nullable<decimal> PaidSumFc { get; set; }
        public Nullable<decimal> PaidSumSc { get; set; }
        public string FolioPref { get; set; }
        public Nullable<int> FolioNum { get; set; }
        public Nullable<decimal> DpmAppl { get; set; }
        public Nullable<decimal> DpmApplFc { get; set; }
        public Nullable<decimal> DpmApplSc { get; set; }
        public Nullable<int> LPgFolioN { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
        public string Posted { get; set; }
        public Nullable<int> OwnerCode { get; set; }
        public string BPChCode { get; set; }
        public Nullable<int> BPChCntc { get; set; }
        public string PayToCode { get; set; }
        public string IsPaytoBnk { get; set; }
        public string BnkCntry { get; set; }
        public string BankCode { get; set; }
        public string BnkAccount { get; set; }
        public string BnkBranch { get; set; }
        public string isIns { get; set; }
        public string TrackNo { get; set; }
        public string VersionNum { get; set; }
        public Nullable<int> LangCode { get; set; }
        public string BPNameOW { get; set; }
        public string BillToOW { get; set; }
        public string ShipToOW { get; set; }
        public string RetInvoice { get; set; }
        public Nullable<System.DateTime> ClsDate { get; set; }
        public Nullable<int> MInvNum { get; set; }
        public Nullable<System.DateTime> MInvDate { get; set; }
        public Nullable<short> SeqCode { get; set; }
        public Nullable<int> Serial { get; set; }
        public string SeriesStr { get; set; }
        public string SubStr { get; set; }
        public string Model { get; set; }
        public Nullable<decimal> TaxOnExp { get; set; }
        public Nullable<decimal> TaxOnExpFc { get; set; }
        public Nullable<decimal> TaxOnExpSc { get; set; }
        public Nullable<decimal> TaxOnExAp { get; set; }
        public Nullable<decimal> TaxOnExApF { get; set; }
        public Nullable<decimal> TaxOnExApS { get; set; }
        public string LastPmnTyp { get; set; }
        public Nullable<int> LndCstNum { get; set; }
        public string UseCorrVat { get; set; }
        public string BlkCredMmo { get; set; }
        public string OpenForLaC { get; set; }
        public string Excised { get; set; }
        public Nullable<System.DateTime> ExcRefDate { get; set; }
        public string ExcRmvTime { get; set; }
        public Nullable<decimal> SrvGpPrcnt { get; set; }
        public Nullable<int> DepositNum { get; set; }
        public string CertNum { get; set; }
        public string DutyStatus { get; set; }
        public string AutoCrtFlw { get; set; }
        public Nullable<System.DateTime> FlwRefDate { get; set; }
        public string FlwRefNum { get; set; }
        public Nullable<int> VatJENum { get; set; }
        public Nullable<decimal> DpmVat { get; set; }
        public Nullable<decimal> DpmVatFc { get; set; }
        public Nullable<decimal> DpmVatSc { get; set; }
        public Nullable<decimal> DpmAppVat { get; set; }
        public Nullable<decimal> DpmAppVatF { get; set; }
        public Nullable<decimal> DpmAppVatS { get; set; }
        public string InsurOp347 { get; set; }
        public string IgnRelDoc { get; set; }
        public string BuildDesc { get; set; }
        public string ResidenNum { get; set; }
        public Nullable<int> Checker { get; set; }
        public Nullable<int> Payee { get; set; }
        public Nullable<int> CopyNumber { get; set; }
        public string SSIExmpt { get; set; }
        public Nullable<int> PQTGrpSer { get; set; }
        public Nullable<int> PQTGrpNum { get; set; }
        public string PQTGrpHW { get; set; }
        public string ReopOriDoc { get; set; }
        public string ReopManCls { get; set; }
        public string DocManClsd { get; set; }
        public Nullable<short> ClosingOpt { get; set; }
        public Nullable<System.DateTime> SpecDate { get; set; }
        public string Ordered { get; set; }
        public string NTSApprov { get; set; }
        public Nullable<short> NTSWebSite { get; set; }
        public string NTSeTaxNo { get; set; }
        public string NTSApprNo { get; set; }
        public string PayDuMonth { get; set; }
        public Nullable<short> ExtraMonth { get; set; }
        public Nullable<short> ExtraDays { get; set; }
        public Nullable<short> CdcOffset { get; set; }
        public string SignMsg { get; set; }
        public string SignDigest { get; set; }
        public string CertifNum { get; set; }
        public Nullable<int> KeyVersion { get; set; }
        public string EDocGenTyp { get; set; }
        public Nullable<short> ESeries { get; set; }
        public string EDocNum { get; set; }
        public Nullable<int> EDocExpFrm { get; set; }
        public string OnlineQuo { get; set; }
        public string POSEqNum { get; set; }
        public string POSManufSN { get; set; }
        public Nullable<int> POSCashN { get; set; }
        public string EDocStatus { get; set; }
        public string EDocCntnt { get; set; }
        public string EDocProces { get; set; }
        public string EDocErrCod { get; set; }
        public string EDocErrMsg { get; set; }
        public string EDocCancel { get; set; }
        public string EDocTest { get; set; }
        public string EDocPrefix { get; set; }
        public Nullable<int> CUP { get; set; }
        public Nullable<int> CIG { get; set; }
        public string DpmAsDscnt { get; set; }
        public string Attachment { get; set; }
        public Nullable<int> AtcEntry { get; set; }
        public string SupplCode { get; set; }
        public string GTSRlvnt { get; set; }
        public Nullable<decimal> BaseDisc { get; set; }
        public Nullable<decimal> BaseDiscSc { get; set; }
        public Nullable<decimal> BaseDiscFc { get; set; }
        public Nullable<decimal> BaseDiscPr { get; set; }
        public Nullable<int> CreateTS { get; set; }
        public Nullable<int> UpdateTS { get; set; }
        public string SrvTaxRule { get; set; }
        public Nullable<int> AnnInvDecR { get; set; }
        public string Supplier { get; set; }
        public Nullable<int> Releaser { get; set; }
        public Nullable<int> Receiver { get; set; }
        public string ToWhsCode { get; set; }
        public Nullable<System.DateTime> AssetDate { get; set; }
        public string Requester { get; set; }
        public string ReqName { get; set; }
        public Nullable<short> Branch { get; set; }
        public Nullable<short> Department { get; set; }
        public string Email { get; set; }
        public string Notify { get; set; }
        public Nullable<int> ReqType { get; set; }
        public string OriginType { get; set; }
        public string IsReuseNum { get; set; }
        public string IsReuseNFN { get; set; }
        public string DocDlvry { get; set; }
        public Nullable<decimal> PaidDpm { get; set; }
        public Nullable<decimal> PaidDpmF { get; set; }
        public Nullable<decimal> PaidDpmS { get; set; }
        public Nullable<int> EnvTypeNFe { get; set; }
        public Nullable<int> AgrNo { get; set; }
        public string IsAlt { get; set; }
        public Nullable<int> AltBaseTyp { get; set; }
        public Nullable<int> AltBaseEnt { get; set; }
        public string AuthCode { get; set; }
        public Nullable<System.DateTime> StDlvDate { get; set; }
        public Nullable<int> StDlvTime { get; set; }
        public Nullable<System.DateTime> EndDlvDate { get; set; }
        public Nullable<int> EndDlvTime { get; set; }
        public string VclPlate { get; set; }
        public string ElCoStatus { get; set; }
        public string AtDocType { get; set; }
        public string ElCoMsg { get; set; }
        public string PrintSEPA { get; set; }
        public Nullable<decimal> FreeChrg { get; set; }
        public Nullable<decimal> FreeChrgFC { get; set; }
        public Nullable<decimal> FreeChrgSC { get; set; }
        public Nullable<decimal> NfeValue { get; set; }
        public string FiscDocNum { get; set; }
        public Nullable<int> RelatedTyp { get; set; }
        public Nullable<int> RelatedEnt { get; set; }
        public Nullable<int> CCDEntry { get; set; }
        public Nullable<int> NfePrntFo { get; set; }
        public Nullable<int> ZrdAbs { get; set; }
        public Nullable<int> POSRcptNo { get; set; }
        public Nullable<decimal> FoCTax { get; set; }
        public Nullable<decimal> FoCTaxFC { get; set; }
        public Nullable<decimal> FoCTaxSC { get; set; }
        public Nullable<int> TpCusPres { get; set; }
        public Nullable<System.DateTime> ExcDocDate { get; set; }
        public Nullable<decimal> FoCFrght { get; set; }
        public Nullable<decimal> FoCFrghtFC { get; set; }
        public Nullable<decimal> FoCFrghtSC { get; set; }
        public Nullable<short> InterimTyp { get; set; }
        public string PTICode { get; set; }
        public string Letter { get; set; }
        public Nullable<int> FolNumFrom { get; set; }
        public Nullable<int> FolNumTo { get; set; }
        public Nullable<int> FolSeries { get; set; }
        public Nullable<decimal> SplitTax { get; set; }
        public Nullable<decimal> SplitTaxFC { get; set; }
        public Nullable<decimal> SplitTaxSC { get; set; }
        public string ToBinCode { get; set; }
        public string PriceMode { get; set; }
        public string PoDropPrss { get; set; }
        public string PermitNo { get; set; }
        public string MYFtype { get; set; }
        public string DocTaxID { get; set; }
        public Nullable<System.DateTime> DateReport { get; set; }
        public string RepSection { get; set; }
        public string ExclTaxRep { get; set; }
        public Nullable<int> PosCashReg { get; set; }
        public string DmpTransID { get; set; }
        public string ECommerBP { get; set; }
        public string EComerGSTN { get; set; }
        public string Revision { get; set; }
        public string RevRefNo { get; set; }
        public Nullable<System.DateTime> RevRefDate { get; set; }
        public string RevCreRefN { get; set; }
        public Nullable<System.DateTime> RevCreRefD { get; set; }
        public string TaxInvNo { get; set; }
        public Nullable<System.DateTime> FrmBpDate { get; set; }
        public string GSTTranTyp { get; set; }
        public Nullable<int> BaseType { get; set; }
        public Nullable<int> BaseEntry { get; set; }
        public string ComTrade { get; set; }
        public string UseBilAddr { get; set; }
        public Nullable<short> IssReason { get; set; }
        public string ComTradeRt { get; set; }
        public string SplitPmnt { get; set; }
        public Nullable<int> SOIWizId { get; set; }
        public string SelfPosted { get; set; }
        public string EnBnkAcct { get; set; }
        public string EncryptIV { get; set; }
        public string U_BPP_CDAD { get; set; }
        public Nullable<System.DateTime> U_BPP_DPFC { get; set; }
        public string U_BPP_DPNM { get; set; }
        public string U_BPP_MDBI { get; set; }
        public string U_BPP_MDCD { get; set; }
        public string U_BPP_MDCO { get; set; }
        public string U_BPP_MDCT { get; set; }
        public string U_BPP_MDDT { get; set; }
        public string U_BPP_MDFC { get; set; }
        public Nullable<System.DateTime> U_BPP_MDFD { get; set; }
        public Nullable<System.DateTime> U_BPP_MDFE { get; set; }
        public string U_BPP_MDFN { get; set; }
        public string U_BPP_MDIA { get; set; }
        public string U_BPP_MDMT { get; set; }
        public string U_BPP_MDND { get; set; }
        public string U_BPP_MDNT { get; set; }
        public string U_BPP_MDOM { get; set; }
        public string U_BPP_MDRT { get; set; }
        public string U_BPP_MDSD { get; set; }
        public string U_BPP_MDSO { get; set; }
        public string U_BPP_MDTD { get; set; }
        public string U_BPP_MDTO { get; set; }
        public string U_BPP_MDTS { get; set; }
        public string U_BPP_MDVC { get; set; }
        public string U_BPP_MDVN { get; set; }
        public string U_BPP_MDVT { get; set; }
        public string U_BPP_OPER { get; set; }
        public string U_SYP_STATUS { get; set; }
        public string U_SYP_GREMISION { get; set; }
        public string U_SYP_INCISO_APLICA { get; set; }
        public string U_syp_tpoimp { get; set; }
        public Nullable<System.DateTime> U_SYP_FECHAREF { get; set; }
        public string U_TipCompra { get; set; }
        public string U_SYP_TmovIng { get; set; }
        public string U_SYP_TEntrega { get; set; }
        public Nullable<int> U_SYP_DOCDT { get; set; }
        public string U_SYP_NROOC { get; set; }
        public string U_syp_olin { get; set; }
        public string U_SYP_DocVta { get; set; }
        public string U_SYP_EMBARC { get; set; }
        public Nullable<System.DateTime> U_SYP_FLLMERC { get; set; }
        public string U_SYP_STATUSIMP { get; set; }
        public string U_SYP_CARPIMP { get; set; }
        public string U_SYP_CINSC { get; set; }
        public Nullable<decimal> U_SYP_MONCCH { get; set; }
        public Nullable<decimal> U_SYP_SALDOCC { get; set; }
        public string U_TIPTRA { get; set; }
        public string U_SYP_ADDON_ANUL { get; set; }
        public string U_SYP_NROCOT { get; set; }
        public string U_SYP_NROPCL { get; set; }
        public string U_SYP_NUMOCCL { get; set; }
        public string U_SYP_TIMPO { get; set; }
        public string U_SYP_CODCL { get; set; }
        public string U_SYP_NOMCL { get; set; }
        public string U_SYP_MTVCOMP { get; set; }
        public string U_SYP_DESDOC { get; set; }
        public Nullable<System.DateTime> U_SYP_MDFC { get; set; }
        public string U_SYP_CODT { get; set; }
        public string U_SYP_NOMT { get; set; }
        public string U_BPP_MDSN { get; set; }
        public string U_SYP_REFE { get; set; }
        public string U_SYP_NOMCONS { get; set; }
        public string U_NroOper { get; set; }
        public string U_SYP_TEMBAR { get; set; }
        public Nullable<System.DateTime> U_SYP_FECHAUTOR { get; set; }
        public string U_SYP_MDAO { get; set; }
        public string U_SYP_MDPLL { get; set; }
        public string U_SYP_SERIESUC { get; set; }
        public string U_SYP_SERTRET { get; set; }
        public string U_SYP_CORCERT { get; set; }
        public string U_SYP_SUCCERT { get; set; }
        public string U_SYP_SERIESUCO { get; set; }
        public Nullable<decimal> U_SYP_TOTALF { get; set; }
        public string U_SYP_CODIDTR { get; set; }
        public string U_SYP_MDPP { get; set; }
        public string U_SYP_ACTISUS { get; set; }
        public string U_SYP_FINES { get; set; }
        public string U_SYP_FORMAP { get; set; }
        public string U_SYP_ADTPAGO { get; set; }
        public string U_SYP_PESRET { get; set; }
        public string U_SYP_PAISP { get; set; }
        public string U_SYP_NROSOLIC { get; set; }
        public string U_SYP_PUERTOC { get; set; }
        public string U_SYP_PUERTOD { get; set; }
        public string U_SYP_REFRENDO { get; set; }
        public string U_SYP_USRSOLC { get; set; }
        public string U_SYP_NUSRSOLC { get; set; }
        public Nullable<int> U_SYP_NROFAB { get; set; }
        public string U_SYP_LPEXPO { get; set; }
        public string U_SYP_TIPONC { get; set; }
        public Nullable<short> U_SYP_HORAE { get; set; }
        public string U_SYP_NROCONT { get; set; }
        public string U_SYP_PERSRET { get; set; }
        public string U_SYP_OBSEXPO { get; set; }
        public string U_SYP_NROCCH { get; set; }
        public string U_SYP_SAUTOR { get; set; }
        public string U_SYP_REFRENDO_CORR { get; set; }
        public string U_SYP_FE_ESTADO { get; set; }
        public string U_SYP_FE_REPROCESAR { get; set; }
        public string U_SYP_RUTA { get; set; }
        public string U_SYP_EXPORTACION { get; set; }
        public string U_SYP_EXPORTACIONDE { get; set; }
        public string U_SYP_ANIOREF { get; set; }
        public Nullable<decimal> U_SYP_VALORFOB { get; set; }
        public string U_SYP_DISTADUANERO { get; set; }
        public string U_SYP_REGIMEN { get; set; }
        public string U_SYP_TRANSPREF { get; set; }
        public Nullable<int> U_SYP_FACREEMBOLSO { get; set; }
        public string U_SYP_FE_ERROR_SRI { get; set; }
        public string U_SYP_FE_ADJUNTO { get; set; }
        public Nullable<System.DateTime> U_SYP_FECAUTOC { get; set; }
        public string U_SYP_PAGOREGFIS { get; set; }
        public Nullable<decimal> U_SYP_TOTBASEREEMB { get; set; }
        public string U_SYP_PAISICE { get; set; }
        public string U_SYP_TIPOPAGO { get; set; }
        public string U_SYP_TIPOREGI { get; set; }
        public string U_SYP_PAISPAGOGEN { get; set; }
        public string U_SYP_PAISPAG_PARFIS { get; set; }
        public string U_SYP_TIP_INGEXT { get; set; }
        public string U_SYP_DENOPAGO { get; set; }
        public string U_SYP_INGEXT_GRVOTRP { get; set; }
        public Nullable<decimal> U_SYP_IMP_OTRPAIS { get; set; }
        public string U_SYP_CMPSLD { get; set; }
        public string U_DAC_NUMGARANTIA { get; set; }
        public Nullable<System.DateTime> U_DAC_FBL { get; set; }
        public Nullable<System.DateTime> U_U_SYS_FECHADESPACHO { get; set; }
        public string U_SYP_ESTADO_FE_GR { get; set; }
        public string U_SYP_INCOTERM { get; set; }
        public string U_DC_NROAUTO { get; set; }
        public string U_SYP_NROAUTO { get; set; }
        public string U_SYP_NROAUTOO { get; set; }
        public string U_SYP_NROAUTOC { get; set; }
        public string U_DC_KILOS { get; set; }
        public string U_DC_COMISION { get; set; }
        public string U_DC_PRODUCTOS { get; set; }
    }
}
