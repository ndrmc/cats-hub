using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.Repository;

namespace DRMFSS.BLL
{

		public class UnitOfWork : IUnitOfWork, IDisposable
		{
            DRMFSSEntities1 db = new DRMFSSEntities1();
 private IAccountRepository _iAccountRepository = null;
           public IAccountRepository Account {
                get{
                        if( _iAccountRepository == null){
                            _iAccountRepository = new AccountRepository( db, this );
                        }
                        return _iAccountRepository;
                    }
            set{
                    _iAccountRepository = value;
                }
        }
          
         private IAdjustmentRepository _iAdjustmentRepository = null;
           public IAdjustmentRepository Adjustment {
                get{
                        if( _iAdjustmentRepository == null){
                            _iAdjustmentRepository = new AdjustmentRepository( db, this );
                        }
                        return _iAdjustmentRepository;
                    }
            set{
                    _iAdjustmentRepository = value;
                }
        }
          
         private IAdjustmentReasonRepository _iAdjustmentReasonRepository = null;
           public IAdjustmentReasonRepository AdjustmentReason {
                get{
                        if( _iAdjustmentReasonRepository == null){
                            _iAdjustmentReasonRepository = new AdjustmentReasonRepository( db, this );
                        }
                        return _iAdjustmentReasonRepository;
                    }
            set{
                    _iAdjustmentReasonRepository = value;
                }
        }
          
         private IAdminUnitRepository _iAdminUnitRepository = null;
           public IAdminUnitRepository AdminUnit {
                get{
                        if( _iAdminUnitRepository == null){
                            _iAdminUnitRepository = new AdminUnitRepository( db, this );
                        }
                        return _iAdminUnitRepository;
                    }
            set{
                    _iAdminUnitRepository = value;
                }
        }
          
         private IAdminUnitTypeRepository _iAdminUnitTypeRepository = null;
           public IAdminUnitTypeRepository AdminUnitType {
                get{
                        if( _iAdminUnitTypeRepository == null){
                            _iAdminUnitTypeRepository = new AdminUnitTypeRepository( db, this );
                        }
                        return _iAdminUnitTypeRepository;
                    }
            set{
                    _iAdminUnitTypeRepository = value;
                }
        }
          
         private IAuditRepository _iAuditRepository = null;
           public IAuditRepository Audit {
                get{
                        if( _iAuditRepository == null){
                            _iAuditRepository = new AuditRepository( db, this );
                        }
                        return _iAuditRepository;
                    }
            set{
                    _iAuditRepository = value;
                }
        }
          
         private ICommodityRepository _iCommodityRepository = null;
           public ICommodityRepository Commodity {
                get{
                        if( _iCommodityRepository == null){
                            _iCommodityRepository = new CommodityRepository( db, this );
                        }
                        return _iCommodityRepository;
                    }
            set{
                    _iCommodityRepository = value;
                }
        }
          
         private ICommodityGradeRepository _iCommodityGradeRepository = null;
           public ICommodityGradeRepository CommodityGrade {
                get{
                        if( _iCommodityGradeRepository == null){
                            _iCommodityGradeRepository = new CommodityGradeRepository( db, this );
                        }
                        return _iCommodityGradeRepository;
                    }
            set{
                    _iCommodityGradeRepository = value;
                }
        }
          
         private ICommoditySourceRepository _iCommoditySourceRepository = null;
           public ICommoditySourceRepository CommoditySource {
                get{
                        if( _iCommoditySourceRepository == null){
                            _iCommoditySourceRepository = new CommoditySourceRepository( db, this );
                        }
                        return _iCommoditySourceRepository;
                    }
            set{
                    _iCommoditySourceRepository = value;
                }
        }
          
         private ICommodityTypeRepository _iCommodityTypeRepository = null;
           public ICommodityTypeRepository CommodityType {
                get{
                        if( _iCommodityTypeRepository == null){
                            _iCommodityTypeRepository = new CommodityTypeRepository( db, this );
                        }
                        return _iCommodityTypeRepository;
                    }
            set{
                    _iCommodityTypeRepository = value;
                }
        }
          
         private IContactRepository _iContactRepository = null;
           public IContactRepository Contact {
                get{
                        if( _iContactRepository == null){
                            _iContactRepository = new ContactRepository( db, this );
                        }
                        return _iContactRepository;
                    }
            set{
                    _iContactRepository = value;
                }
        }
          
         private IDetailRepository _iDetailRepository = null;
           public IDetailRepository Detail {
                get{
                        if( _iDetailRepository == null){
                            _iDetailRepository = new DetailRepository( db, this );
                        }
                        return _iDetailRepository;
                    }
            set{
                    _iDetailRepository = value;
                }
        }
          
         private IDispatchRepository _iDispatchRepository = null;
           public IDispatchRepository Dispatch {
                get{
                        if( _iDispatchRepository == null){
                            _iDispatchRepository = new DispatchRepository( db, this );
                        }
                        return _iDispatchRepository;
                    }
            set{
                    _iDispatchRepository = value;
                }
        }
          
         private IDispatchAllocationRepository _iDispatchAllocationRepository = null;
           public IDispatchAllocationRepository DispatchAllocation {
                get{
                        if( _iDispatchAllocationRepository == null){
                            _iDispatchAllocationRepository = new DispatchAllocationRepository( db, this );
                        }
                        return _iDispatchAllocationRepository;
                    }
            set{
                    _iDispatchAllocationRepository = value;
                }
        }
          
         private IDispatchDetailRepository _iDispatchDetailRepository = null;
           public IDispatchDetailRepository DispatchDetail {
                get{
                        if( _iDispatchDetailRepository == null){
                            _iDispatchDetailRepository = new DispatchDetailRepository( db, this );
                        }
                        return _iDispatchDetailRepository;
                    }
            set{
                    _iDispatchDetailRepository = value;
                }
        }
          
         private IDonorRepository _iDonorRepository = null;
           public IDonorRepository Donor {
                get{
                        if( _iDonorRepository == null){
                            _iDonorRepository = new DonorRepository( db, this );
                        }
                        return _iDonorRepository;
                    }
            set{
                    _iDonorRepository = value;
                }
        }
          
         private IFDPRepository _iFDPRepository = null;
           public IFDPRepository FDP {
                get{
                        if( _iFDPRepository == null){
                            _iFDPRepository = new FDPRepository( db, this );
                        }
                        return _iFDPRepository;
                    }
            set{
                    _iFDPRepository = value;
                }
        }
          
         private IForgetPasswordRequestRepository _iForgetPasswordRequestRepository = null;
           public IForgetPasswordRequestRepository ForgetPasswordRequest {
                get{
                        if( _iForgetPasswordRequestRepository == null){
                            _iForgetPasswordRequestRepository = new ForgetPasswordRequestRepository( db, this );
                        }
                        return _iForgetPasswordRequestRepository;
                    }
            set{
                    _iForgetPasswordRequestRepository = value;
                }
        }
          
         private IGiftCertificateRepository _iGiftCertificateRepository = null;
           public IGiftCertificateRepository GiftCertificate {
                get{
                        if( _iGiftCertificateRepository == null){
                            _iGiftCertificateRepository = new GiftCertificateRepository( db, this );
                        }
                        return _iGiftCertificateRepository;
                    }
            set{
                    _iGiftCertificateRepository = value;
                }
        }
          
         private IGiftCertificateDetailRepository _iGiftCertificateDetailRepository = null;
           public IGiftCertificateDetailRepository GiftCertificateDetail {
                get{
                        if( _iGiftCertificateDetailRepository == null){
                            _iGiftCertificateDetailRepository = new GiftCertificateDetailRepository( db, this );
                        }
                        return _iGiftCertificateDetailRepository;
                    }
            set{
                    _iGiftCertificateDetailRepository = value;
                }
        }
          
         private IHubRepository _iHubRepository = null;
           public IHubRepository Hub {
                get{
                        if( _iHubRepository == null){
                            _iHubRepository = new HubRepository( db, this );
                        }
                        return _iHubRepository;
                    }
            set{
                    _iHubRepository = value;
                }
        }
          
         private IHubOwnerRepository _iHubOwnerRepository = null;
           public IHubOwnerRepository HubOwner {
                get{
                        if( _iHubOwnerRepository == null){
                            _iHubOwnerRepository = new HubOwnerRepository( db, this );
                        }
                        return _iHubOwnerRepository;
                    }
            set{
                    _iHubOwnerRepository = value;
                }
        }
          
         private IHubSettingRepository _iHubSettingRepository = null;
           public IHubSettingRepository HubSetting {
                get{
                        if( _iHubSettingRepository == null){
                            _iHubSettingRepository = new HubSettingRepository( db, this );
                        }
                        return _iHubSettingRepository;
                    }
            set{
                    _iHubSettingRepository = value;
                }
        }
          
         private IHubSettingValueRepository _iHubSettingValueRepository = null;
           public IHubSettingValueRepository HubSettingValue {
                get{
                        if( _iHubSettingValueRepository == null){
                            _iHubSettingValueRepository = new HubSettingValueRepository( db, this );
                        }
                        return _iHubSettingValueRepository;
                    }
            set{
                    _iHubSettingValueRepository = value;
                }
        }
          
         private IInternalMovementRepository _iInternalMovementRepository = null;
           public IInternalMovementRepository InternalMovement {
                get{
                        if( _iInternalMovementRepository == null){
                            _iInternalMovementRepository = new InternalMovementRepository( db, this );
                        }
                        return _iInternalMovementRepository;
                    }
            set{
                    _iInternalMovementRepository = value;
                }
        }
          
         private ILedgerRepository _iLedgerRepository = null;
           public ILedgerRepository Ledger {
                get{
                        if( _iLedgerRepository == null){
                            _iLedgerRepository = new LedgerRepository( db, this );
                        }
                        return _iLedgerRepository;
                    }
            set{
                    _iLedgerRepository = value;
                }
        }
          
         private ILedgerTypeRepository _iLedgerTypeRepository = null;
           public ILedgerTypeRepository LedgerType {
                get{
                        if( _iLedgerTypeRepository == null){
                            _iLedgerTypeRepository = new LedgerTypeRepository( db, this );
                        }
                        return _iLedgerTypeRepository;
                    }
            set{
                    _iLedgerTypeRepository = value;
                }
        }
          
         private ILetterTemplateRepository _iLetterTemplateRepository = null;
           public ILetterTemplateRepository LetterTemplate {
                get{
                        if( _iLetterTemplateRepository == null){
                            _iLetterTemplateRepository = new LetterTemplateRepository( db, this );
                        }
                        return _iLetterTemplateRepository;
                    }
            set{
                    _iLetterTemplateRepository = value;
                }
        }
          
         private IMasterRepository _iMasterRepository = null;
           public IMasterRepository Master {
                get{
                        if( _iMasterRepository == null){
                            _iMasterRepository = new MasterRepository( db, this );
                        }
                        return _iMasterRepository;
                    }
            set{
                    _iMasterRepository = value;
                }
        }
          
         private IOtherDispatchAllocationRepository _iOtherDispatchAllocationRepository = null;
           public IOtherDispatchAllocationRepository OtherDispatchAllocation {
                get{
                        if( _iOtherDispatchAllocationRepository == null){
                            _iOtherDispatchAllocationRepository = new OtherDispatchAllocationRepository( db, this );
                        }
                        return _iOtherDispatchAllocationRepository;
                    }
            set{
                    _iOtherDispatchAllocationRepository = value;
                }
        }
          
         private IPartitionRepository _iPartitionRepository = null;
           public IPartitionRepository Partition {
                get{
                        if( _iPartitionRepository == null){
                            _iPartitionRepository = new PartitionRepository( db, this );
                        }
                        return _iPartitionRepository;
                    }
            set{
                    _iPartitionRepository = value;
                }
        }
          
         private IPeriodRepository _iPeriodRepository = null;
           public IPeriodRepository Period {
                get{
                        if( _iPeriodRepository == null){
                            _iPeriodRepository = new PeriodRepository( db, this );
                        }
                        return _iPeriodRepository;
                    }
            set{
                    _iPeriodRepository = value;
                }
        }
          
         private IProgramRepository _iProgramRepository = null;
           public IProgramRepository Program {
                get{
                        if( _iProgramRepository == null){
                            _iProgramRepository = new ProgramRepository( db, this );
                        }
                        return _iProgramRepository;
                    }
            set{
                    _iProgramRepository = value;
                }
        }
          
         private IProjectCodeRepository _iProjectCodeRepository = null;
           public IProjectCodeRepository ProjectCode {
                get{
                        if( _iProjectCodeRepository == null){
                            _iProjectCodeRepository = new ProjectCodeRepository( db, this );
                        }
                        return _iProjectCodeRepository;
                    }
            set{
                    _iProjectCodeRepository = value;
                }
        }
          
         private IReceiptAllocationRepository _iReceiptAllocationRepository = null;
           public IReceiptAllocationRepository ReceiptAllocation {
                get{
                        if( _iReceiptAllocationRepository == null){
                            _iReceiptAllocationRepository = new ReceiptAllocationRepository( db, this );
                        }
                        return _iReceiptAllocationRepository;
                    }
            set{
                    _iReceiptAllocationRepository = value;
                }
        }
          
         private IReceiveRepository _iReceiveRepository = null;
           public IReceiveRepository Receive {
                get{
                        if( _iReceiveRepository == null){
                            _iReceiveRepository = new ReceiveRepository( db, this );
                        }
                        return _iReceiveRepository;
                    }
            set{
                    _iReceiveRepository = value;
                }
        }
          
         private IReceiveDetailRepository _iReceiveDetailRepository = null;
           public IReceiveDetailRepository ReceiveDetail {
                get{
                        if( _iReceiveDetailRepository == null){
                            _iReceiveDetailRepository = new ReceiveDetailRepository( db, this );
                        }
                        return _iReceiveDetailRepository;
                    }
            set{
                    _iReceiveDetailRepository = value;
                }
        }
          
         private IReleaseNoteRepository _iReleaseNoteRepository = null;
           public IReleaseNoteRepository ReleaseNote {
                get{
                        if( _iReleaseNoteRepository == null){
                            _iReleaseNoteRepository = new ReleaseNoteRepository( db, this );
                        }
                        return _iReleaseNoteRepository;
                    }
            set{
                    _iReleaseNoteRepository = value;
                }
        }
          
         private IRoleRepository _iRoleRepository = null;
           public IRoleRepository Role {
                get{
                        if( _iRoleRepository == null){
                            _iRoleRepository = new RoleRepository( db, this );
                        }
                        return _iRoleRepository;
                    }
            set{
                    _iRoleRepository = value;
                }
        }
          
         private ISessionAttemptRepository _iSessionAttemptRepository = null;
           public ISessionAttemptRepository SessionAttempt {
                get{
                        if( _iSessionAttemptRepository == null){
                            _iSessionAttemptRepository = new SessionAttemptRepository( db, this );
                        }
                        return _iSessionAttemptRepository;
                    }
            set{
                    _iSessionAttemptRepository = value;
                }
        }
          
         private ISessionHistoryRepository _iSessionHistoryRepository = null;
           public ISessionHistoryRepository SessionHistory {
                get{
                        if( _iSessionHistoryRepository == null){
                            _iSessionHistoryRepository = new SessionHistoryRepository( db, this );
                        }
                        return _iSessionHistoryRepository;
                    }
            set{
                    _iSessionHistoryRepository = value;
                }
        }
          
         private ISettingRepository _iSettingRepository = null;
           public ISettingRepository Setting {
                get{
                        if( _iSettingRepository == null){
                            _iSettingRepository = new SettingRepository( db, this );
                        }
                        return _iSettingRepository;
                    }
            set{
                    _iSettingRepository = value;
                }
        }
          
         private IShippingInstructionRepository _iShippingInstructionRepository = null;
           public IShippingInstructionRepository ShippingInstruction {
                get{
                        if( _iShippingInstructionRepository == null){
                            _iShippingInstructionRepository = new ShippingInstructionRepository( db, this );
                        }
                        return _iShippingInstructionRepository;
                    }
            set{
                    _iShippingInstructionRepository = value;
                }
        }
          
         private ISMSRepository _iSMSRepository = null;
           public ISMSRepository SMS {
                get{
                        if( _iSMSRepository == null){
                            _iSMSRepository = new SMSRepository( db, this );
                        }
                        return _iSMSRepository;
                    }
            set{
                    _iSMSRepository = value;
                }
        }
          
         private IStackEventRepository _iStackEventRepository = null;
           public IStackEventRepository StackEvent {
                get{
                        if( _iStackEventRepository == null){
                            _iStackEventRepository = new StackEventRepository( db, this );
                        }
                        return _iStackEventRepository;
                    }
            set{
                    _iStackEventRepository = value;
                }
        }
          
         private IStackEventTypeRepository _iStackEventTypeRepository = null;
           public IStackEventTypeRepository StackEventType {
                get{
                        if( _iStackEventTypeRepository == null){
                            _iStackEventTypeRepository = new StackEventTypeRepository( db, this );
                        }
                        return _iStackEventTypeRepository;
                    }
            set{
                    _iStackEventTypeRepository = value;
                }
        }
          
         private IStoreRepository _iStoreRepository = null;
           public IStoreRepository Store {
                get{
                        if( _iStoreRepository == null){
                            _iStoreRepository = new StoreRepository( db, this );
                        }
                        return _iStoreRepository;
                    }
            set{
                    _iStoreRepository = value;
                }
        }
          
         private ITransactionRepository _iTransactionRepository = null;
           public ITransactionRepository Transaction {
                get{
                        if( _iTransactionRepository == null){
                            _iTransactionRepository = new TransactionRepository( db, this );
                        }
                        return _iTransactionRepository;
                    }
            set{
                    _iTransactionRepository = value;
                }
        }
          
         private ITransactionGroupRepository _iTransactionGroupRepository = null;
           public ITransactionGroupRepository TransactionGroup {
                get{
                        if( _iTransactionGroupRepository == null){
                            _iTransactionGroupRepository = new TransactionGroupRepository( db, this );
                        }
                        return _iTransactionGroupRepository;
                    }
            set{
                    _iTransactionGroupRepository = value;
                }
        }
          
         private ITranslationRepository _iTranslationRepository = null;
           public ITranslationRepository Translation {
                get{
                        if( _iTranslationRepository == null){
                            _iTranslationRepository = new TranslationRepository( db, this );
                        }
                        return _iTranslationRepository;
                    }
            set{
                    _iTranslationRepository = value;
                }
        }
          
         private ITransporterRepository _iTransporterRepository = null;
           public ITransporterRepository Transporter {
                get{
                        if( _iTransporterRepository == null){
                            _iTransporterRepository = new TransporterRepository( db, this );
                        }
                        return _iTransporterRepository;
                    }
            set{
                    _iTransporterRepository = value;
                }
        }
          
         private IUnitRepository _iUnitRepository = null;
           public IUnitRepository Unit {
                get{
                        if( _iUnitRepository == null){
                            _iUnitRepository = new UnitRepository( db, this );
                        }
                        return _iUnitRepository;
                    }
            set{
                    _iUnitRepository = value;
                }
        }
          
         private IUserHubRepository _iUserHubRepository = null;
           public IUserHubRepository UserHub {
                get{
                        if( _iUserHubRepository == null){
                            _iUserHubRepository = new UserHubRepository( db, this );
                        }
                        return _iUserHubRepository;
                    }
            set{
                    _iUserHubRepository = value;
                }
        }
          
         private IUserProfileRepository _iUserProfileRepository = null;
           public IUserProfileRepository UserProfile {
                get{
                        if( _iUserProfileRepository == null){
                            _iUserProfileRepository = new UserProfileRepository( db, this );
                        }
                        return _iUserProfileRepository;
                    }
            set{
                    _iUserProfileRepository = value;
                }
        }
          
         private IUserRoleRepository _iUserRoleRepository = null;
           public IUserRoleRepository UserRole {
                get{
                        if( _iUserRoleRepository == null){
                            _iUserRoleRepository = new UserRoleRepository( db, this );
                        }
                        return _iUserRoleRepository;
                    }
            set{
                    _iUserRoleRepository = value;
                }
        }
          
             

            public void Dispose()
            {
                this.db.Dispose();
            }
	   }
}


